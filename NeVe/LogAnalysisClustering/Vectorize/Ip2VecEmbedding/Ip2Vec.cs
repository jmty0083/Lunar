using LogAnalysisLibrary;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model;
using System.ComponentModel;
using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Vectorize.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.Misc;

namespace LogAnalysisClustering.Vectorize.Ip2VecEmbedding
{
    public class Ip2Vec
    {
        public Ip2VecSettings Ip2VecSettings { get; set; } = Ip2VecSettings.Default;

        public List<Sentence> Data { get; set; }

        public double Loss { get; private set; } = 0d;

        public Ip2VecModel Ip2VecModel { get; set; }

        private int WordCount { get; set; }

        private const int tableSize = (int)1e8;

        private int[] DiceTable { get; set; } = new int[tableSize];

        private int Iter { get; set; } = 0;

        private static readonly int recordingThreshold = 20;

        private const string directory = Constants.DefaultDebugLoggingDirectory;

        private int W2I(string word) => this.Ip2VecModel.Word2Index[word];

        //private Matrix<double> W1 { get; set; }

        //private Matrix<double> W2 { get; set; }

#if DEBUG
        private Random random { get; set; } = new Random(Constants.DebugSeed);
#else
        private Random random { get; set; } = new Random();
#endif

        public void Vectorize()
        {
            this.PreTrain();
            //this.Ip2VecModel.PrintToFile(directory + "pre.txt");
            this.Train();
            //this.Ip2VecModel.PrintToFile(directory + "train.txt");
        }

        private void Train()
        {
            using (var status = StatusWrapper.NewStatus(@"Train", this.Data.Count() * this.Ip2VecSettings.EpochsCount))
            {
                // Building training container
                var sets = new List<TrainingElement>();
                foreach (var set in this.Ip2VecSettings.ContextRelationDict)
                {
                    var target = TrainingElement.NewContainer(this.WordCount, set.Value.Count);
                    sets.Add(target);
                }

                for (int i = 0; i < this.Ip2VecSettings.EpochsCount; i++)
                {
                    this.Loss = 0d;
                    foreach (var item in this.Data)
                    {
                        //this.StatusController.Preparesw.Start();
                        // Building training elements
                        var c = 0;
                        foreach (var set in this.Ip2VecSettings.ContextRelationDict)
                        {
                            sets[c].RebuildTargetWordVector(W2I(set.Key(item)));
                            sets[c].SetContextWordVector(set.Value.Select(x => W2I(x(item))).ToArray());
                            c++;
                        }

                        //this.StatusController.Preparesw.Stop();
                        //this.StatusController.Calcsw.Start();
                        var index = 0;
                        if (this.Ip2VecSettings.NegativeSampling > 0)
                        {
                            foreach (var set in sets)
                            {
                                this.TrainElementWithNegativeSampling(set);
                            }
                        }
                        else
                        {
                            foreach (var set in sets)
                            {
                                this.TrainElement(set, index++);
                            }
                        }

                        Iter++;
                        status.PushProgress();
                        //this.StatusController.Calcsw.Stop();
                        //this.StatusController.SetCurrentProgress();
                        //if (Iter % 1000 == 0)
                        //{
                        //    this.StatusController.Refresh();
                        //}

                        //this.Percentage.Value = Iter;
                        //if (this.W1.ToRowMajorArray().Contains(double.NaN))
                        //{
                        //    MessageBox.Show("NAN error");
                        //}
                    }
                    Console.WriteLine("loss = " + this.Loss);
                }
#if DEBUG
                var sb = new StringBuilder();
                foreach (var item in this.Ip2VecModel.Index2Word)
                {
                    sb.AppendLine(string.Format("{0}\t{1}", item.Key, item.Value));
                }
            (new Thread(Record)).Start(new KeyValuePair<string, string>("Dictionary", sb.ToString()));
#endif
            }
        }

        private void PreTrain()
        {
            //Distinct
            this.Data = this.Data.Distinct().ToList();

            using (var status = StatusWrapper.NewStatus(@"PreTrain", this.Data.Count))
            {
                // Model Initialization if needed
                if (this.Ip2VecModel == null)
                {
                    this.Ip2VecModel = new Ip2VecModel();
                }

                // Negative Sampling Count
                //var freq = new Dictionary<string, int>();


                // Generating trainning data
                // Generating dictionraies
                int index = this.Ip2VecModel.Word2Index.Count;
                foreach (var item in this.Data)
                {
                    var allfunc = this.Ip2VecSettings.ContextRelationDict.SelectMany(x => x.Value).ToList();
                    allfunc.AddRange(this.Ip2VecSettings.ContextRelationDict.Keys.ToList());

                    foreach (var func in allfunc.Distinct())
                    {
                        var value = func.Invoke(item);

                        if (!this.Ip2VecModel.Word2Index.ContainsKey(value))
                        {
                            this.Ip2VecModel.Word2Index.Add(value, index);
                            this.Ip2VecModel.Index2Word.Add(index, value);
                            index++;

                            this.Ip2VecModel.Frequency.Add(1);
                        }
                        else
                        {
                            this.Ip2VecModel.Frequency[this.Ip2VecModel.Word2Index[value]]++;
                        }
                    }
                    status.PushProgress();
                }

                this.WordCount = index;
            }


            //if (this.Ip2VecSettings.UseRandomInitialization)
            //{
            // Appending Learning Mode w/o new word
            if (this.Ip2VecModel.W1 != null && this.WordCount == this.Ip2VecModel.W1.RowCount)
            {
                this.BuildDiceTable();
                return;
            }

            this.Ip2VecModel.W1 = Matrix<double>.Build.Dense(this.WordCount, this.Ip2VecSettings.NeuronCount);
            this.Ip2VecModel.W2 = Matrix<double>.Build.Dense(this.WordCount, this.Ip2VecSettings.NeuronCount);

            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = this.Ip2VecSettings.MaximumParallelWorker
            };
            var result = Parallel.For(0, this.Ip2VecSettings.MaximumParallelWorker, parallelOptions, this.RandomInitializationParallelWorker);

            if (!result.IsCompleted)
            {
                throw new InvalidOperationException();
            }

            //if (this.Ip2VecModel.W1 != null && this.Ip2VecModel.W2 != null)
            //{
            //    for (int i = 0; i < this.Ip2VecModel.W1.RowCount; i++)
            //    {
            //        this.W1.SetRow(i, this.Ip2VecModel.W1.Row(i));
            //        this.W2.SetRow(i, this.Ip2VecModel.W2.Row(i));
            //    }
            //}

            //this.Ip2VecModel.W1 = this.W1;
            //this.Ip2VecModel.W2 = this.W2;

            this.BuildDiceTable();
            //}
            //else
            //{
            //    this.Ip2VecModel.W1 = Matrix<double>.Build.Dense(this.WordCount, this.Ip2VecSettings.NeuronCount, (i, j) => 1d / (i * 10d + 2));
            //    this.Ip2VecModel.W2 = Matrix<double>.Build.Dense(this.WordCount, this.Ip2VecSettings.NeuronCount, (i, j) => 1d / (i * 10d + 2));
            //}

        }

        private void RandomInitializationParallelWorker(int threadid)
        {
            int interval = Convert.ToInt32(Math.Ceiling(((double)this.WordCount) / this.Ip2VecSettings.MaximumParallelWorker));

            for (int i = threadid * interval; i < interval + threadid * interval; i++)
            {
                if (i >= this.WordCount)
                {
                    break;
                }
                var row1 = Vector<double>.Build.Dense(this.Ip2VecSettings.NeuronCount, j => this.random.NextDouble());
                var row2 = Vector<double>.Build.Dense(this.Ip2VecSettings.NeuronCount, j => this.random.NextDouble());
                this.Ip2VecModel.W1.SetRow(i, row1);
                this.Ip2VecModel.W2.SetRow(i, row2);
            }
        }


        // Nagetive sampling 
        // Building Dice Table
        private void BuildDiceTable()
        {
            if (this.Ip2VecSettings.NegativeSampling > 0)
            {
                var power = 0.75;
                var norm = this.Ip2VecModel.Frequency.Sum(x => Math.Pow(x, power));
                var p = 0d;
                var t = 0;
                for (int i = 0; i < this.Ip2VecModel.Frequency.Count; i++)
                {
                    p += Math.Pow(this.Ip2VecModel.Frequency[i], power) / norm;
                    while (t < tableSize && (double)t / tableSize < p)
                    {
                        this.DiceTable[t] = i;
                        t++;
                    }
                }
            }
        }

        private void TrainElementWithNegativeSampling(TrainingElement trainset)
        {
            foreach (var contextWordIndex in trainset.ContextWordIndexList)
            {
                var deltaW1 = Vector<double>.Build.Dense(Ip2VecSettings.NeuronCount, 0);
                for (int i = 0; i < this.Ip2VecSettings.NegativeSampling + 1; i++)
                {
                    int targetNegativeWordIndex;
                    int label;
                    if (i == 0)
                    {
                        targetNegativeWordIndex = contextWordIndex;
                        label = 1;
                    }
                    else
                    {
                        targetNegativeWordIndex = this.DiceTable[random.Next(0, tableSize)];
                        if (targetNegativeWordIndex == contextWordIndex)
                        {
                            continue;
                        }
                        label = 0;
                    }

                    this.NegativeSamplingIteration(contextWordIndex, label, targetNegativeWordIndex, ref deltaW1);
                }
                this.Ip2VecModel.W1.SetRow(contextWordIndex, this.Ip2VecModel.W1.Row(contextWordIndex).Add(deltaW1));
            }
        }

        private void NegativeSamplingIteration(int contextWordIndex, int label, int negativeWordIndex, ref Vector<double> deltaW1)
        {
            var targetWordVector = this.Ip2VecModel.W1.Row(contextWordIndex);
            var negativeWordW2Vector = this.Ip2VecModel.W2.Row(negativeWordIndex);
            var f = Sigmoid(targetWordVector.DotProduct(negativeWordW2Vector));
            var g = (label - f) * this.Ip2VecSettings.LearningRate;

            deltaW1 = deltaW1.Add(negativeWordW2Vector.Multiply(g));

            //var t = this.W2.Transpose();
            this.Ip2VecModel.W2.SetRow(negativeWordIndex, negativeWordW2Vector.Add(targetWordVector.Multiply(g)));
            //this.W2 = t.Transpose();

            if (label == 0)
            {
                this.Loss -= Math.Log(Sigmoid(-targetWordVector.DotProduct(negativeWordW2Vector)));
            }
            else
            {
                this.Loss -= Math.Log(f);
            }
            //Console.WriteLine(this.Loss);
        }

        private void TrainElement(TrainingElement trainset, int subIter)
        {
            var sb = new StringBuilder("********************new iteration starts************************").AppendLine();
            var h = this.Ip2VecModel.W1.Transpose().Multiply(trainset.TargetWordVector);
            var u = this.Ip2VecModel.W2.Multiply(h);
            var y_pred = this.Softmax(u);

            var EI = Vector<double>.Build.Dense(this.WordCount, 0);
            foreach (var item in trainset.ContextWordVectorList)
            {
                EI = EI.Add(y_pred.Subtract(item));
            }
            sb.Append("h   >>>").Append(h.ToString(int.MaxValue, int.MaxValue)).AppendLine();
            sb.Append("u   >>>").Append(u.ToString(int.MaxValue, int.MaxValue)).AppendLine();
            sb.Append("y_p >>>").Append(y_pred.ToString(int.MaxValue, int.MaxValue)).AppendLine();
            sb.Append("EI  >>>").Append(EI.ToString(int.MaxValue, int.MaxValue)).AppendLine();

            var delta_dw2 = h.OuterProduct(EI);
            var delta_dw1 = trainset.TargetWordVector.OuterProduct(this.Ip2VecModel.W2.Transpose().Multiply(EI.ToColumnMatrix()).Column(0));

            this.Ip2VecModel.W1 -= delta_dw1.Multiply(this.Ip2VecSettings.LearningRate);
            this.Ip2VecModel.W2 = (this.Ip2VecModel.W2.Transpose() - delta_dw2.Multiply(this.Ip2VecSettings.LearningRate)).Transpose();


            sb.AppendLine("W1   >>>").Append(Ip2VecModel.W1.ToString(int.MaxValue, int.MaxValue)).AppendLine();
            sb.AppendLine("W2   >>>").Append(Ip2VecModel.W2.ToString(int.MaxValue, int.MaxValue)).AppendLine();


            sb.AppendLine("************************iteration ends***************************");

#if DEBUG
            var data = new KeyValuePair<string, string>(string.Format("{0}-{1}", this.Iter.ToString(), subIter.ToString()), sb.ToString());
            new Thread(Record).Start(data);
#endif
            //this.Record(sb.ToString());
        }

        public void SaveCsvMatrix(string filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, false))
            {
                foreach (var item in this.Ip2VecModel.W1.ToRowArrays())
                {
                    file.WriteLine(string.Join(",", item));
                }
            }
        }

        private static void Record(object content)
        {
            var data = (KeyValuePair<string, string>)content;
            string filename = string.Format("{0}{1}.txt", directory, data.Key.ToString());
            Console.WriteLine(filename);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, false))
            {
                file.WriteLine(data.Value);
            }

            if (data.Key.Contains("-"))
            {
                var iter = int.Parse(data.Key.Split('-')[0]);
                var deleting = string.Format("{0}{1}", directory, (iter - recordingThreshold).ToString());
                var d0 = deleting + "-0.txt";
                var d1 = deleting + "-1.txt";
                var d2 = deleting + "-2.txt";
                if (iter - recordingThreshold > 0 && File.Exists(d0) && File.Exists(d1) && File.Exists(d2))
                {
                    try
                    {
                        File.Delete(d0);
                        File.Delete(d1);
                        File.Delete(d2);
                    }
                    catch (Exception e)
                    {
                        Logback.Log(e.Message);
                    }
                }
            }
        }

        private Vector<double> Softmax(Vector<double> vector)
        {
            //e_x = np.exp(x - np.max(x))
            //return e_x / e_x.sum(axis = 0)
            var exp = vector.Subtract(vector.Max()).ToArray();
            var e_x = Vector<double>.Build.Dense(vector.Count, i => Math.Exp(exp[i]));
            return e_x.Divide(e_x.Sum());
        }

        private static double Sigmoid(double value)
        {
            return 1.0d / (1.0d + Math.Exp(-value));
        }

    }

    public class TrainingElement
    {
        public Vector<double> TargetWordVector { get; private set; }

        public int TargetWordIndex { get; private set; }

        public List<Vector<double>> ContextWordVectorList { get; private set; }

        public List<int> ContextWordIndexList { get; private set; }

        private int Count { get; set; }

        public TrainingElement()
        {
            this.ContextWordVectorList = new List<Vector<double>>();
            this.ContextWordIndexList = new List<int>();
        }

        public static TrainingElement NewInstance(int count)
        {
            return new TrainingElement { Count = count };
        }

        public static TrainingElement NewContainer(int count, int contextCount)
        {
            var container = new TrainingElement { Count = count }.BuildTargetWordVector(0);
            for (int i = 0; i < contextCount; i++)
            {
                container = container.AddContextWordVector(0);
            }
            return container;
        }

        public TrainingElement BuildTargetWordVector(int index)
        {
            double[] vec = new double[this.Count];
            vec[index] = 1;
            this.TargetWordVector = Vector<double>.Build.DenseOfArray(vec);
            this.TargetWordIndex = index;
            return this;
        }

        public void RebuildTargetWordVector(int index)
        {
            this.TargetWordVector[this.TargetWordIndex] = 0;
            this.TargetWordVector[index] = 1;
            this.TargetWordIndex = index;
        }

        public TrainingElement AddContextWordVector(int index)
        {
            double[] vec = new double[this.Count];
            vec[index] = 1;
            this.ContextWordVectorList.Add(Vector<double>.Build.DenseOfArray(vec));
            this.ContextWordIndexList.Add(index);
            return this;
        }

        public void SetContextWordVector(int[] indices)
        {
            if (indices.Length != this.ContextWordIndexList.Count)
            {
                throw new ArgumentOutOfRangeException("Cannot reset context word vector: incorrect length of indices");
            }

            for (int i = 0; i < indices.Length; i++)
            {
                this.ContextWordVectorList[i][this.ContextWordIndexList[i]] = 0;
                this.ContextWordVectorList[i][indices[i]] = 1;
                this.ContextWordIndexList[i] = indices[i];
            }
        }
    }

}
