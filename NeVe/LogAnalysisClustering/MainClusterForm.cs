using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model;
using LogAnalysisClustering.Vectorize.NeVeEmbedding.Model;
using LogAnalysisClustering.Vectorize.StringEmbedding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogAnalysisClustering
{
    public partial class MainClusterForm : Form
    {
        public MainController MainController { get; set; } = new MainController();

        public MainClusterForm()
        {
            InitializeComponent();
            StatusController.ProgressBar = this.ProgressBar;
            StatusController.TimeEstimationLabel = this.TimeEstimationLabel;
            StatusController.TimeUsageLabel = this.TimeUsageLabel;
            StatusController.ProgressLabel = this.ProgressLabel;
            StatusController.PercentageLabel = this.PercentageLabel;
            StatusController.StatusLabel = this.StatusLabel;
            StatusController.WorkerLabel = this.WorkerLabel;
            StatusController.Initialize();
        }

        private void AddFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var readFileDialog = new OpenFileDialog()
            {
                Filter = "CSV (*.csv)|*.csv"
            };
            if (readFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.MainController.DataController.AddFile(readFileDialog.FileName);
            }
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog()
            {
                SelectedPath = Constants.DefaultCsvBrowsingPath
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.MainController.DataController.AddFiles(folderBrowserDialog.SelectedPath);
            }
        }

        private void EmbeddingButton_Click(object sender, EventArgs e)
        {
            if (this.Ip2VecEmbeddingButton.Checked)
            {
                this.MainController.BeginIp2VecVectorize();
            }
            else if (this.NeVeEmbeddingButton.Checked)
            {
                this.MainController.BeginGloVeVectorize();
            }         
        }

        private void KDisCalcButton_Click(object sender, EventArgs e)
        {
            IEmbeddingModel model;
            if (this.Ip2VecEmbeddingButton.Checked)
            {
                model = new Ip2VecModel();
            }
            else if(this.NeVeEmbeddingButton.Checked)
            {
                model = new NeVeModel();
            }
            else
            {
                model = new StringEmbeddingModel();
            }
            MainController.BeginKDisCurveCalculator(model);
        }

        private void DBScanButton_Click(object sender, EventArgs e)
        {
            IEmbeddingModel model;
            if (this.Ip2VecEmbeddingButton.Checked)
            {
                model = new Ip2VecModel();
            }
            else if (this.NeVeEmbeddingButton.Checked)
            {
                model = new NeVeModel();
            }
            else
            {
                model = new StringEmbeddingModel();
            }
            MainController.BeginDBScan(model);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var file = @"E:\EventExtractionExam\Debug\Research Y_categorized.csv";
            this.MainController.DataController.AddFile(file);
        }

        private void MeasureButton_Click(object sender, EventArgs e)
        {
            IEmbeddingModel model;
            if (this.Ip2VecEmbeddingButton.Checked)
            {
                model = new Ip2VecModel();
            }
            else if(this.NeVeEmbeddingButton.Checked)
            {
                model = new NeVeModel();
            }
            else
            {
                model = new StringEmbeddingModel();
            }
            this.MainController.BeginMeasurement(model);
        }

        private void UniversalProcessButton_Click(object sender, EventArgs e)
        {
            IEmbeddingModel model;
            DoWorkEventHandler handler;
            if (this.Ip2VecEmbeddingButton.Checked)
            {
                model = new Ip2VecModel();
                handler = this.MainController.VectorizeController.Ip2VecEmbeddingWorker;
            }
            else if (this.NeVeEmbeddingButton.Checked)
            {
                model = new NeVeModel();
                handler = this.MainController.VectorizeController.NeVeEmbeddingWorker;
            }
            else
            {
                model = new StringEmbeddingModel();
                handler = null;
            }

            var times = Convert.ToInt32(this.RepetitionNumericCount.Value);
            this.MainController.BeginEasyAccess(times, model, handler);
        }

        private void tSNEButton_Click(object sender, EventArgs e)
        {
            IEmbeddingModel model;
            if (this.Ip2VecEmbeddingButton.Checked)
            {
                model = new Ip2VecModel();
            }
            else if (this.NeVeEmbeddingButton.Checked)
            {
                model = new NeVeModel();
            }
            else
            {
                model = new StringEmbeddingModel();
            }
            this.MainController.BeginTSNETransform(model);
        }
    }
}
