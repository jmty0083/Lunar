using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.DataType;
using LogAnalysisClustering.Vectorize.Misc;
using LogAnalysisClustering.Vectorize.NeVeEmbedding.Model;
using LogAnalysisLibrary.Systems;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Vectorize.NeVeEmbedding
{
    public partial class NeVe
    {
        public NeVeSettings NeVeSettings { get; set; }

        public NeVeModel NeVeModel { get; set; }

        public List<Sentence> Data { get; set; }

        private List<double> MainBias { get; set; } = new List<double>();

        private List<double> ContextBias { get; set; } = new List<double>();

        private List<Vector<double>> MainGradientSquared { get; set; } = new List<Vector<double>>();

        private List<Vector<double>> ContextGradientSquared { get; set; } = new List<Vector<double>>();

        private List<double> MainGradientSquaredBias { get; set; } = new List<double>();

        private List<double> ContextGradientSquaredBias { get; set; } = new List<double>();

        //public Matrix<double> CooccurrenceMatrix { get; set; }

        private List<Tuple<int, int, double>> Cooccurrences { get; set; }

        private int WordCount { get; set; }


        private double Loss { get; set; }

        private object LossLock { get; set; } = new object();

        public void Vectorize()
        {
            this.PreTrain();
            this.Train();
        }

        private void PreTrain()
        {
            // Distinct
            this.Data = this.Data.Distinct().ToList();

            this.NeVeModel = new NeVeModel();

            StatusWrapper.NewStatus("PreTrain");

            // WordCount
            int index = 0;

            this.NeVeModel.Word2Index = this.Data
                .SelectMany(x => Contexts.ContextFuncDict.Values.Select(y => y.Invoke(x)))
                .Distinct()
                .ToDictionary(x => x, x => index++);

            this.WordCount = index;

            // Build Cooccurrences
            var cooccurrenceMatrix = new Dictionary<Coordinate<int,int>, double>();
            Coordinate<int,int> coordinate;
            foreach (var sentence in this.Data)
            {
                foreach (var relations in this.NeVeSettings.ContextRelationDistanceDict)
                {
                    foreach (var distancefunc in relations.Value)
                    {
                        //var increment = 1d / distancefunc.Value;
                        //var i = this.Word2Index[relations.Key.Invoke(sentence)];
                        //var j = this.Word2Index[distancefunc.Key.Invoke(sentence)];
                        coordinate = Coordinate<int, int>.NewInstance(this.NeVeModel.Word2Index[relations.Key.Invoke(sentence)]
                            , this.NeVeModel.Word2Index[distancefunc.Key.Invoke(sentence)]);

                        if (cooccurrenceMatrix.ContainsKey(coordinate))
                        {
                            cooccurrenceMatrix[coordinate] += 1d / distancefunc.Value;
                        }
                        else
                        {
                            cooccurrenceMatrix.Add(coordinate, 1d / distancefunc.Value);
                        }
                        //cooccurrenceMatrix[]
                        //cooccurrenceMatrix[this.GloVeModel.Word2Index[relations.Key.Invoke(sentence)]
                        //    , this.GloVeModel.Word2Index[distancefunc.Key.Invoke(sentence)]] += 1d / distancefunc.Value;
                    }
                }
            }

            // Build Working Tuples
            this.Cooccurrences = cooccurrenceMatrix
                .Select(x => new Tuple<int, int, double>(x.Key.X, x.Key.Y, x.Value)).ToList();
            //this.Cooccurrences.Shuffle();

            // Build Word Vectors
#if DEBUG
            var r = new Random(Constants.DebugSeed);
#else
            var r = new Random();
#endif
            for (int i = 0; i < index; i++)
            {
                //var row = Vector<double>.Build.Dense(this.GloVeSettings.VectorSize, x => r.NextDouble());
                this.NeVeModel.MainVectors.Add(Vector<double>.Build.Dense(this.NeVeSettings.VectorSize, x => (r.NextDouble() - 0.5) / (index + 1)));
                this.NeVeModel.ContextVectors.Add(Vector<double>.Build.Dense(this.NeVeSettings.VectorSize, x => (r.NextDouble() - 0.5) / (index + 1)));
                this.MainBias.Add((r.NextDouble() - 0.5) / index + 1);
                this.ContextBias.Add((r.NextDouble() - 0.5) / index + 1);
                this.MainGradientSquared.Add(Vector<double>.Build.Dense(this.NeVeSettings.VectorSize, 1));
                this.ContextGradientSquared.Add(Vector<double>.Build.Dense(this.NeVeSettings.VectorSize, 1));
                this.MainGradientSquaredBias.Add(1);
                this.ContextGradientSquaredBias.Add(1);
            }
        }
        
        //using AdaGradient
        private void Train()
        {
            // Status
            using (var status = StatusWrapper.NewStatus(@"Train", this.Cooccurrences.Count * this.NeVeSettings.EpochsCount))
            {
                for (int epoch = 0; epoch < this.NeVeSettings.EpochsCount; epoch++)
                {
                    this.Loss = 0d;

                    ParallelOptions parallelOptions = new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = this.NeVeSettings.MaximumParallelWorker
                    };
                    var result = Parallel.For(0, this.NeVeSettings.MaximumParallelWorker, parallelOptions, id => this.AdaGradient(id, status));

                    if (!result.IsCompleted)
                    {
                        throw new InvalidOperationException();
                    }

                    Console.WriteLine("Loss = {0}", this.Loss);
                }
            }
        }

        private void AdaGradient(int threadid, StatusWrapper status)
        {
            int i, j;
            double co;

            var weight = 0d;
            var innercost = 0d;

            Vector<double> gradMain, gradContext;
            double gradBias;

            int interval = Convert.ToInt32(Math.Ceiling(((double)this.Cooccurrences.Count) / this.NeVeSettings.MaximumParallelWorker));
            for (int t = threadid * interval; t < interval + threadid * interval && t < this.Cooccurrences.Count; t++)
            {
                i = this.Cooccurrences[t].Item1;
                j = this.Cooccurrences[t].Item2;
                co = this.Cooccurrences[t].Item3;

                lock (this.NeVeModel.MainVectors[i])
                {
                    lock (this.NeVeModel.ContextVectors[j])
                    {
                        weight = co < this.NeVeSettings.XMax ? Math.Pow(co / this.NeVeSettings.XMax, .75) : 1;

                        innercost = this.NeVeModel.MainVectors[i].DotProduct(this.NeVeModel.ContextVectors[j]) + this.MainBias[i] + this.ContextBias[j] - Math.Log(co);

                        lock (this.LossLock)
                        {
                            this.Loss += 0.5 * (weight * innercost * innercost);
                        }
                        //Console.WriteLine(this.Loss);
                        //if (double.IsNaN(this.Loss))
                        //{
                        //    Console.WriteLine(this.Loss);
                        //}

                        gradBias = weight * innercost;
                        gradMain = gradBias * this.NeVeModel.ContextVectors[j];
                        gradContext = gradBias * this.NeVeModel.MainVectors[i];

                        this.NeVeModel.MainVectors[i] -= (this.NeVeSettings.LearningRate * gradMain / this.MainGradientSquared[i].PointwiseSqrt());
                        this.NeVeModel.ContextVectors[j] -= (this.NeVeSettings.LearningRate * gradContext / this.ContextGradientSquared[j].PointwiseSqrt());

                        this.MainBias[i] -= (this.NeVeSettings.LearningRate * gradBias / Math.Sqrt(this.MainGradientSquaredBias[i]));
                        this.ContextBias[j] -= (this.NeVeSettings.LearningRate * gradBias / Math.Sqrt(this.ContextGradientSquaredBias[j]));

                        this.MainGradientSquared[i] += gradMain.PointwisePower(2);
                        this.ContextGradientSquared[j] += gradContext.PointwisePower(2);
                        this.MainGradientSquaredBias[i] += gradBias * gradBias;
                        this.ContextGradientSquaredBias[j] += gradBias * gradBias;

                        
                        //Console.WriteLine(i + ";" + string.Join(",", this.GloVeModel.MainVectors[i]));
                        //Console.WriteLine(j + ";" + string.Join(",", this.GloVeModel.ContextVectors[j]));
                    }
                }

                status.PushProgressSafe();
            }
        }
    }
}
