using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalysisClustering.DataType
{
    public class StatusWrapper : IDisposable
    {
        public string CurrentStatus { get; private set; }

        public long CurrentProgress;

        public long TotalProgress;

        //private object ProgressLock { get; set; } = new object();

        public static StatusWrapper NewStatus(string status)
        {
            var statusWrapper = new StatusWrapper
            {
                CurrentStatus = status,
            };

            StatusController.Display(statusWrapper);

            return statusWrapper;
        }

        public static StatusWrapper NewStatus(string status, long totalProgress)
        {
            var statusWrapper = new StatusWrapper
            {
                CurrentStatus = status,
                CurrentProgress = 0,
                TotalProgress = totalProgress,
            };

            StatusController.Start(statusWrapper);

            return statusWrapper;
        }

        public void Dispose()
        {
            StatusController.Stop();
        }

        public void PushProgressSafe()
        {
            //lock (this.ProgressLock)
            //{
#if DEBUG
                if (this.CurrentProgress + 1 > this.TotalProgress)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Progress exceeds maximum : {0} + {1} > {2}", this.CurrentProgress, 1, this.TotalProgress));
                }
#endif
            Interlocked.Increment(ref this.CurrentProgress);
            //this.CurrentProgress += step;
            //}
        }

        public void PushProgress(int step = 1)
        {
            this.CurrentProgress += step;
        }

    }
}
