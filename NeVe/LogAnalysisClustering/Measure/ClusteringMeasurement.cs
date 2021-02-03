using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize;
using LogAnalysisLibrary.Data.Constants;
using LogAnalysisLibrary.DataType;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Measure
{
    public class ClusteringMeasurement
    {
        //public DataController DataController { get; set; }

        public List<KeyFeatureClustered> ResultList { get; set; }

        public List<KeyFeatureOfRandomEvent> LabelList { get; set; }

        public ClusterExternalValidation ExternalValidation { get; private set; } = new ClusterExternalValidation();

        //public IEmbeddingModel EmbeddingModel { get; set; }

        private List<Tuple<int, int>> ComparisonList { get; set; } = new List<Tuple<int, int>>();


        public void Measure()
        {
            ComparisonList.Clear();

            //foreach (var filename in DataController.Files)
            //{
            //StatusWrapper.NewStatus("Preparing");
            //BuildOriginalLists(filename);
            if (ResultList.Count != LabelList.Count)
            {
                throw new ArgumentException("Lists are not paired");
            }

            BuildComparisonList();
            CountErrors();
            //}
        }

        //private void BuildOriginalLists(string filename)
        //{
        //    LabelList = DataController.UseData<KeyFeatureOfRandomEvent>(filename).ToList();
        //    ResultList = DataController.UseData<KeyFeatureClustered>(EmbeddingModel.GetClusteredFilename(filename)).ToList();
        //}

        private void BuildComparisonList()
        {
            //var mx = 0;
            for (int i = 0; i < ResultList.Count; i++)
            {
                if (!Validation(ResultList[i], LabelList[i]))
                {
                    throw new Exception("Errors in two lists");
                }

                if (LabelList[i].RandomType == EventType.Noise.ToString())
                {
                    ComparisonList.Add(new Tuple<int, int>(0, ResultList[i].ClusterId));
                }
                else
                {
                    ComparisonList.Add(new Tuple<int, int>(int.Parse(LabelList[i].EventId), ResultList[i].ClusterId));
                    //mx = Math.Max(mx, int.Parse(this.LabelList[i].EventId));
                }
            }
            //return mx;
        }

        private void CountErrors()
        {
            using (var status = StatusWrapper.NewStatus("Counting", ComparisonList.Count * ((long)ComparisonList.Count - 1) / 2))
            {
                for (int i = 0; i < ComparisonList.Count - 1; i++)
                {
                    for (int j = i + 1; j < ComparisonList.Count; j++)
                    {
                        status.PushProgress();

                        //if (i == j)
                        //{
                        //    continue;
                        //}

                        if (ComparisonList[i].Item1 == ComparisonList[j].Item1)
                        {
                            if (ComparisonList[i].Item2 == ComparisonList[j].Item2)
                            {
                                ExternalValidation.TruePositive++;
                            }
                            else
                            {
                                ExternalValidation.FalseNegative++;
                            }
                        }
                        else
                        {
                            if (ComparisonList[i].Item2 == ComparisonList[j].Item2)
                            {
                                ExternalValidation.FalsePositive++;
                            }
                            else
                            {
                                ExternalValidation.TrueNegative++;
                            }
                        }
                    }
                }
            }

            ExternalValidation.Purity = (double)ComparisonList.GroupBy(x => x.Item2)
                .Sum(x => x.GroupBy(y => y.Item1).Max(y => y.Count())) / ComparisonList.Count;

            ExternalValidation.NMI = CalculateNMI();
        }

        private bool Validation(KeyFeature random, KeyFeature clustered)
        {
            return random.ThreatName == clustered.ThreatName &&
                random.SourceIp == random.SourceIp &&
                random.TargetIp == random.TargetIp &&
                random.SourcePort == random.SourcePort &&
                random.TargetPort == random.TargetPort &&
                random.Protocol == random.Protocol &&
                random.Timestamp == random.Timestamp;
        }

        private double CalculateNMI()
        {
            var total = ComparisonList.Count;
            var Bg = LabelList.Max(x => int.Parse(x.EventId)) + 1;
            var Rg = ResultList.Max(x => x.ClusterId) + 1;
            var matrix = Matrix<double>.Build.Dense(Bg, Rg, 0);
            //var matrix = new int[this.LabelList.Max(x => int.Parse(x.EventId)), this.ResultList.Max(x => x.ClusterId)];
            ComparisonList.ForEach(x => matrix[x.Item1, x.Item2] += 1d / total);

            var mi = 0d;
            for (int i = 0; i < Bg; i++)
            {
                for (int j = 0; j < Rg; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        continue;
                    }

                    mi += (double)matrix[i, j] * Math.Log(matrix[i, j] / (matrix.Row(i).Sum() * matrix.Column(j).Sum()));
                }
            }

            var hx = -matrix.RowSums().ToArray().Sum(x => x != 0 ? (double)x * Math.Log((double)x) : 0);
            var hy = -matrix.ColumnSums().ToArray().Sum(y => y != 0 ? (double)y * Math.Log((double)y) : 0);
            return 2 * mi / (hx + hy);
        }
    }
}
