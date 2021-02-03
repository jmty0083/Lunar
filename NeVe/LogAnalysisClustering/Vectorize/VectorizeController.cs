using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.DataType;
using LogAnalysisClustering.Vectorize.NeVeEmbedding;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model;
using LogAnalysisClustering.Vectorize.NeVeEmbedding.Model;
using LogAnalysisLibrary.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Vectorize
{
    public class VectorizeController
    {
        public DataController DataController { get; set; }

        public Ip2VecSettings Ip2VecSettings { get; private set; } = Ip2VecSettings.Default;

        public NeVeSettings NeVeSettings { get; private set; } = NeVeSettings.Default;

        public void Ip2VecEmbeddingWorker(object sender, DoWorkEventArgs e)
        {
            StatusController.WorkerStatus(true);
            //var modelpath = e.Argument as string;
            List<Sentence> sentences;

            foreach (var filename in this.DataController.Files)
            {
                sentences = this.DataController.UseData<KeyFeature>(filename).Select(x => new Sentence(x)).ToList();

                var ip2vec = new Ip2Vec
                {
                    Ip2VecSettings = this.Ip2VecSettings,
                    Data = sentences,
                };
                //if (!string.IsNullOrEmpty(modelpath))
                //{
                //    var model = new Ip2VecModel();
                //    model.ReadModel(modelpath);
                //    ip2vec.Ip2VecModel = model;
                //}
                var time = new Stopwatch();
                time.Start();
                ip2vec.Vectorize();
                time.Stop();
                Console.WriteLine("{0} from {1}", time.Elapsed.TotalSeconds.ToString("R"), filename);

                ip2vec.Ip2VecModel.SaveModel(Definitions.GetIp2VecModelFilename(filename));

                //this.ClusteringSets[this.DataController.CurrentDataFilename].Ip2VecModel = ip2vec.Ip2VecModel;

                sentences.Clear();
            }

            //this.StatusController.SetCurrentStatusName("待命");
            StatusController.WorkerStatus(false);
        }

        public void NeVeEmbeddingWorker(object sender, DoWorkEventArgs e)
        {
            StatusController.WorkerStatus(true);

            var sentences = new List<Sentence>();
            foreach (var filename in this.DataController.Files)
            {
                sentences = this.DataController.UseData<KeyFeature>(filename).Select(x => new Sentence(x)).ToList();

                var neve = new NeVe
                {
                    NeVeSettings = this.NeVeSettings,
                    Data = sentences,
                };

                var time = new Stopwatch();
                time.Start();
                neve.Vectorize();
                time.Stop();
                Console.WriteLine("{0} from {1}", time.Elapsed.TotalSeconds.ToString("R"), filename);

                try
                {
                    neve.NeVeModel.SaveModel(Definitions.GetNeVeModelFilename(filename));
                    Console.WriteLine("Finish saving");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //ip2vec.Ip2VecModel.SaveModel(Constants.DefaultIp2VecDebugLoggingDirectory + this.DataController.CurrentDataName + Constants.DefaultIp2VecModelSuffix);

                //this.ClusteringSets[this.DataController.CurrentDataFilename].Ip2VecModel = ip2vec.Ip2VecModel;

                sentences.Clear();
            }
            //this.StatusController.SetCurrentStatusName("待命");
            StatusController.WorkerStatus(false);
        }
    }
}
