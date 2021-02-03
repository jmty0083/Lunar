using LogAnalysisClustering.Cluster;
using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize;
using LogAnalysisClustering.Vectorize.NeVeEmbedding.Model;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding;
using LogAnalysisLibrary.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalysisClustering
{
    public class MainController
    {
        public DataController DataController { get; set; }

        public VectorizeController VectorizeController { get; set; } 

        public ClusteringController ClusteringController { get; set; }

        public AutoResetEvent AutoResetEvent { get; set; } = new AutoResetEvent(false);

        public MainController()
        {
            this.DataController = new DataController();
            this.VectorizeController = new VectorizeController { DataController = this.DataController };
            this.ClusteringController = new ClusteringController { DataController = this.DataController };
        }

        public void BeginIp2VecVectorize()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += VectorizeController.Ip2VecEmbeddingWorker;
            worker.RunWorkerAsync();
        }

        public void BeginGloVeVectorize()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += VectorizeController.NeVeEmbeddingWorker;
            worker.RunWorkerAsync();
        }

        public void BeginKDisCurveCalculator(IEmbeddingModel model)
        {
            this.ClusteringController.EmbeddingModel = model;

            var worker = new BackgroundWorker();
            worker.DoWork += ClusteringController.CalcInflectionPointWorker;
            worker.RunWorkerAsync();
        }

        public void BeginDBScan(IEmbeddingModel model)
        {
            this.ClusteringController.EmbeddingModel = model;

            var worker = new BackgroundWorker();
            worker.DoWork += ClusteringController.DBScanWorker;
            worker.RunWorkerAsync();
        }

        public void BeginMeasurement(IEmbeddingModel model)
        {
            this.ClusteringController.EmbeddingModel = model;

            var worker = new BackgroundWorker();
            worker.DoWork += this.ClusteringController.ClusteringMeasurementWorker;
            worker.RunWorkerAsync();
        }

        public void BeginEasyAccess(int times, IEmbeddingModel model, DoWorkEventHandler embeddingHandler)
        {
            this.ClusteringController.EmbeddingModel = model;
            var worker = new BackgroundWorker();
            worker.DoWork += this.EasyAccess;
            worker.RunWorkerAsync(new object[] { times, embeddingHandler});

        }

        public void BeginTSNETransform(IEmbeddingModel model)
        {
            this.ClusteringController.EmbeddingModel = model;

            var worker = new BackgroundWorker();
            worker.DoWork += this.ClusteringController.TSNETransformWorker;
            worker.RunWorkerAsync();
        }

        private void EasyAccess(object sender, DoWorkEventArgs e)
        {
            var args = e.Argument as object[];
            var times = (int)args[0];
            var embeddingHandler = (DoWorkEventHandler)args[1];
            var locker = new AutoResetEvent(true);


            for (int i = 0; i < times; i++)
            {
                locker.WaitOne();

                var worker = new BackgroundWorker();
                worker.DoWork += embeddingHandler;
                worker.DoWork += this.ClusteringController.ProcessingVectorizedDataWorker;
                worker.RunWorkerAsync(locker);
            }
        }
    }
}
