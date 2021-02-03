using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Measure;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize;
using LogAnalysisClustering.Vectorize.StringEmbedding;
using LogAnalysisLibrary.Algorithms.Mathematics;
using LogAnalysisLibrary.Data.Constants;
using LogAnalysisLibrary.DataType;
using LogAnalysisLibrary.DataType.VectorPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Cluster
{
    public class ClusteringController
    {
        public DataController DataController { get; set; }

        public IEmbeddingModel EmbeddingModel { get; set; }

        public void CalcInflectionPointWorker(object sender, DoWorkEventArgs e)
        {
            StatusController.WorkerStatus(true);

            foreach (var filename in this.DataController.Files)
            {
                this.CalcInflectionPointForFile(filename);
            }

            StatusController.WorkerStatus(false);
        }

        public void DBScanWorker(object sender, DoWorkEventArgs e)
        {
            StatusController.WorkerStatus(true);

            foreach (var filename in this.DataController.Files)
            {
                var inflectionPoint = KDisCurveCalculator
                    .ReadInflectionPointList(this.EmbeddingModel.GetInflectionPointName(filename))
                    .First();
                this.DBScanForFileWithInflectionPoint(filename, inflectionPoint);
            }

            StatusController.WorkerStatus(false);
        }

        public void ClusteringMeasurementWorker(object sender, DoWorkEventArgs e)
        {
            StatusController.WorkerStatus(true);
            foreach (var filename in DataController.Files)
            {
                var result = this.ClusteringMeasurementForFile(filename);
                //Console.WriteLine(result.Print());
            }
            StatusController.WorkerStatus(false);
        }

        public void ProcessingVectorizedDataWorker(object sender, DoWorkEventArgs e)
        {
            var locker = e.Argument as AutoResetEvent;
            StatusController.WorkerStatus(true);
            foreach (var filename in this.DataController.Files)
            {
                var ipps = this.CalcInflectionPointForFile(filename,false);
                var resultList = new List<ClusterExternalValidation>();
                foreach (var ipp in ipps)
                {
                    this.DBScanForFileWithInflectionPoint(filename, ipp);
                    var result = this.ClusteringMeasurementForFile(filename);

                    resultList.Add(result);
                }

                //foreach (var item in resultList)
                //{
                //    Console.WriteLine(item.Print());
                //}

                var maxf1 = resultList.Max(x => x.F1score);

                Console.WriteLine(resultList.Where(x => x.F1score == maxf1).First().Print());
            }
            StatusController.WorkerStatus(false);
            locker.Set();
        }

        public void TSNETransformWorker(object sender, DoWorkEventArgs e)
        {
            StatusController.WorkerStatus(true);
            foreach (var filename in DataController.Files)
            {
                this.TSNETransformForFile(filename);
                //Console.WriteLine(result.Print());
            }
            StatusController.WorkerStatus(false);
        }

        private List<InflectionPoint> CalcInflectionPointForFile(string filename, bool save = true)
        {
            this.EmbeddingModel.ReadModel(this.EmbeddingModel.GetModelName(filename));

            var id = 0;
            var kDisCurveCalculator = new KDisCurveCalculator
            {
                Data = this.DataController.UseData<KeyFeature>(filename)
                    .Select(data => data.ToVectorPoint(this.EmbeddingModel, id++)).ToList()
            };

            kDisCurveCalculator.Calculate();

            if (save)
            {
                kDisCurveCalculator.SaveInflectionPointList(this.EmbeddingModel.GetInflectionPointName(filename));
                kDisCurveCalculator.SaveNearestNeighborList(@"E:\EventExtractionExam\Debug\kds.txt");
                //kDisCurveCalculator.GetKDisCurveFigure().SaveImage(@"E:\EventExtractionExam\Debug\figure.png", ImageFormat.Png);
            }

            return kDisCurveCalculator.InflectionPointList;
        }

        private List<KeyFeatureClustered> DBScanForFileWithInflectionPoint(string filename, InflectionPoint inflectionPoint, bool save = true)
        {
            this.EmbeddingModel.ReadModel(this.EmbeddingModel.GetModelName(filename));

            var id = 0;
            var vectors = this.DataController
                .UseData<KeyFeature>(filename)
                .Select(x => (new KeyFeatureClustered(x) { Id = id++ }).ToVectorPoint(this.EmbeddingModel))
                .ToList();

            //var vectors = data.ToList();
            //data = null;
            //GC.Collect();

            //var inflectionPoint = KDisCurveCalculator
            //    .ReadInflectionPointList(this.EmbeddingModel.GetInflectionPointName(filename))
            //    .First();

            var dbscan = new DBScan
            {
                Data = vectors,
                Radius = inflectionPoint.KDisValue,
                MinPts = 4,
                GetDistance = Distance.Manhattan,
                //RangeDiameter = TimeCalculator.MaxCountInTimeSpan(tl, TimeSpan.FromHours(1))// (int)(data.Count / ((tl.Max() - tl.Min()).TotalMinutes) * 60)
            };

            dbscan.Cluster();

            id = 0;
            var data = this.DataController
                .UseData<KeyFeature>(filename)
                .Select(x => new KeyFeatureClustered(x) { Id = id++ })
                .ToList();

            using (StreamWriter file = new StreamWriter(this.EmbeddingModel.GetClusteredFilename(filename), false, Encoding.Default))
            {
                foreach (var line in dbscan.Data)
                {
                    data[line.Id].ClusterId = line.ClusterId;
                    data[line.Id].LinkingId = line.LinkingId;
                    file.WriteLine(data[line.Id].CSV);
                }
            }

            return data;
        }

        private ClusterExternalValidation ClusteringMeasurementForFile(string filename)
        {
            StatusWrapper.NewStatus("Preparing");
            var labelList = DataController.UseData<KeyFeatureOfRandomEvent>(filename).ToList();
            var resultList = DataController.UseData<KeyFeatureClustered>(EmbeddingModel.GetClusteredFilename(filename)).ToList();

            var measure = new ClusteringMeasurement
            {
                LabelList = labelList,
                ResultList = resultList,
            };
            measure.Measure();

            //Console.WriteLine(measure.ExternalValidation.Print());

            return measure.ExternalValidation;
        }

        private void TSNETransformForFile(string filename)
        {
            this.EmbeddingModel.ReadModel(this.EmbeddingModel.GetModelName(filename));

            StatusWrapper.NewStatus("Preparing");
            var visualizer = new TSNEVisualization
            {
                Data = this.DataController.UseData<KeyFeatureClusteredCategorized>(filename).ToList(),
                Model = this.EmbeddingModel
            };
            //Console.WriteLine(measure.ExternalValidation.Print());
            visualizer.toSNE(@"E:\EventExtractionExam\Debug\tSNEcate.csv");
        }


        //public void StringScanWorker(object sender, DoWorkEventArgs e)
        //{
        //    StatusController.WorkerStatus(true);

        //    foreach (var filename in this.DataController.Files)
        //    {
        //        var id = 0;
        //        var data = this.DataController
        //            .UseData<KeyFeature>(filename)
        //            .Select(x => new KeyFeatureClustered(x) { Id = id++ })
        //            .ToList();

        //        var vectors = data.Select(x => new StringVectorPoint
        //        {
        //            //Vector = data.Value,
        //            //Id = data.Key
        //            Vector = new string[] {x.ThreatName,x.SourceIp, x.TargetIp, x.TargetPort, x.Protocol },
        //            TimeTick = DateTime.Parse(x.Timestamp).SecondsTick(),
        //            Id = x.Id
        //        }).ToList();
        //        data = null;
        //        GC.Collect();

        //        var dbscan = new StringScan
        //        {
        //            Data = vectors,
        //            Radius = Constants.StringSimilarityThreshold,
        //            MinPts = 4,
        //            GetDistance = null,
        //            //RangeDiameter = TimeCalculator.MaxCountInTimeSpan(tl, TimeSpan.FromHours(1))// (int)(data.Count / ((tl.Max() - tl.Min()).TotalMinutes) * 60)
        //        };

        //        dbscan.Cluster();

        //        id = 0;
        //        data = this.DataController
        //            .UseData<KeyFeature>(filename)
        //            .Select(x => new KeyFeatureClustered(x) { Id = id++ })
        //            .ToList();

        //        using (StreamWriter file = new StreamWriter(new StringEmbeddingModel().GetClusteredFilename(filename), false, Encoding.Default))
        //        {
        //            foreach (var line in dbscan.Data)
        //            {
        //                data[line.Id].ClusterId = line.ClusterId;
        //                data[line.Id].LinkingId = line.LinkingId;
        //                file.WriteLine(data[line.Id].CSV);
        //            }
        //        }
        //    }

        //    StatusController.WorkerStatus(false);
        //}
    }
}
