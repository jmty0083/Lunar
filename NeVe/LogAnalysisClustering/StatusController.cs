using LogAnalysisClustering.DataType;
using LogAnalysisLibrary.Algorithms.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogAnalysisClustering
{
    public static class StatusController
    {
        public static ProgressBar ProgressBar { get; set; }

        public static Label TimeUsageLabel { get; set; }

        public static Label TimeEstimationLabel { get; set; }

        public static Label StatusLabel { get; set; }

        public static Label ProgressLabel { get; set; }

        public static Label PercentageLabel { get; set; }

        public static Label WorkerLabel { get; set; }

        private static Stopwatch Timer { get; set; } = new Stopwatch();

        private static StatusWrapper Status { get; set; }

        private static BackgroundWorker Worker { get; set; }

        private static AutoResetEvent WorkerInBusy { get; set; } = new AutoResetEvent(true);

        private delegate void InvokeDelegate(object content);

        public static void Initialize()
        {
            Worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            Worker.DoWork += StatusController.DoWork;
            Worker.RunWorkerCompleted += StatusController.RunWorkerCompleted;
        }

        public static void Display(StatusWrapper status)
        {
            StatusLabel.Invoke(new InvokeDelegate(SetStatusLabel), status.CurrentStatus);
        }

        public static void Start(StatusWrapper status)
        {
            //if (Worker != null)
            //{
            //    Console.WriteLine("Force Cancelling Worker");
            //    Worker.CancelAsync();
            //}
            //Console.WriteLine("Wait for Worker Reset");
            WorkerInBusy.WaitOne();
            //Console.WriteLine("Worker Reset!!!");

            Status = status;
            Timer.Restart();

            //ProgressBar.Invoke(new InvokeDelegate(SetProgressBarMaximum), status.TotalProgress);
            StatusLabel.Invoke(new InvokeDelegate(SetStatusLabel), status.CurrentStatus);
            //ProgressBar.Maximum = Status.TotalProgress;
            //StatusLabel.Text = Status.CurrentStatus;
            UpdateProgress();

            Worker.RunWorkerAsync();
        }

        public static void Stop()
        {
            if (Worker != null)
            {
                //Console.WriteLine("On Cancelling Worker");
                Worker.CancelAsync();
            }
        }

        public static void WorkerStatus(bool working)
        {
            WorkerLabel.Invoke(new InvokeDelegate(SetWorkerLabel), working);
        }

        private static void DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            while (worker.CancellationPending != true)
            {
                UpdateProgress();
                Thread.Sleep(1000);
            }
            //Console.WriteLine("Exiting Worker");
        }

        public static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel.Invoke(new InvokeDelegate(SetStatusLabel), "等待开始");
            ProgressLabel.Invoke(new InvokeDelegate(SetProgressLabel), "等待开始");
            PercentageLabel.Invoke(new InvokeDelegate(SetPercentageLabel), "等待开始");
            TimeUsageLabel.Invoke(new InvokeDelegate(SetTimeUsageLabel), "等待开始");
            TimeEstimationLabel.Invoke(new InvokeDelegate(SetTimeEstimationLabel), "等待开始");

            ProgressBar.Invoke(new InvokeDelegate(SetProgressBarValue), 0);
            //ProgressBar.Invoke(new InvokeDelegate(SetProgressBarMaximum), 100);

            //Console.WriteLine("Worker Reset");
            WorkerInBusy.Set();
        }

        private static void UpdateProgress()
        {
            ProgressBar.Invoke(new InvokeDelegate(SetProgressBarValue), (int)((double)Status.CurrentProgress * 10000 / Status.TotalProgress));

            if (Status.TotalProgress > 0)
            {
                ProgressLabel.Invoke(new InvokeDelegate(SetProgressLabel), string.Format("{0}/{1}", Status.CurrentProgress, Status.TotalProgress));
                PercentageLabel.Invoke(new InvokeDelegate(SetPercentageLabel), string.Format("{0:0.##}%", (double)Status.CurrentProgress * 100/Status.TotalProgress));

                TimeUsageLabel.Invoke(new InvokeDelegate(SetTimeUsageLabel), "已用时间：" + TimeSpan.FromMilliseconds(Timer.ElapsedMilliseconds).ToString(@"hh\:mm\:ss\.fff"));

                var time = (double)Timer.ElapsedMilliseconds * (Status.TotalProgress - Status.CurrentProgress) / Status.CurrentProgress;
                if (time.HasValue())
                {
                    TimeEstimationLabel.Invoke(new InvokeDelegate(SetTimeEstimationLabel), "估计剩余时间：" + TimeSpan.FromMilliseconds(time).ToString(@"hh\:mm\:ss\.fff"));
                }
                else
                {
                    TimeEstimationLabel.Invoke(new InvokeDelegate(SetTimeEstimationLabel), "等待开始");
                }
            }
            else
            {
                StatusLabel.Invoke(new InvokeDelegate(SetStatusLabel), Status.CurrentStatus);
                ProgressLabel.Invoke(new InvokeDelegate(SetProgressLabel), "等待开始");
                PercentageLabel.Invoke(new InvokeDelegate(SetPercentageLabel), "等待开始");
                TimeUsageLabel.Invoke(new InvokeDelegate(SetTimeUsageLabel), "等待开始");
                TimeEstimationLabel.Invoke(new InvokeDelegate(SetTimeEstimationLabel), "等待开始");
            }
        }

        private static void SetStatusLabel(object content)
        {
            StatusLabel.Text = content as string;
        }

        private static void SetProgressBarValue(object content)
        {
            ProgressBar.Value = (int)content;
        }

        private static void SetProgressBarMaximum(object content)
        {
            ProgressBar.Maximum = (int)content;
        }

        private static void SetProgressLabel(object content)
        {
            ProgressLabel.Text = content as string;
        }

        private static void SetPercentageLabel(object content)
        {
            PercentageLabel.Text = content as string;
        }

        private static void SetTimeUsageLabel(object content)
        {
            TimeUsageLabel.Text = content as string;
        }

        private static void SetTimeEstimationLabel(object content)
        {
            TimeEstimationLabel.Text = content as string;
        }

        private static void SetWorkerLabel(object content)
        {
            if ((bool) content)
            {
                WorkerLabel.Text = "WORKING";
                WorkerLabel.BackColor = Color.Red;
            }
            else
            {
                WorkerLabel.Text = "IDLE";
                new SoundPlayer(Properties.Resources.superjump).Play();
                WorkerLabel.BackColor = Color.Transparent;
            }
        }
    }
}
