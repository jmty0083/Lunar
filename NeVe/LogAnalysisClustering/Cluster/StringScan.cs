using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisLibrary.Algorithms;
using LogAnalysisLibrary.Algorithms.Mathematics;
using LogAnalysisLibrary.DataType;
using LogAnalysisLibrary.DataType.Helper;
using LogAnalysisLibrary.DataType.VectorPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Cluster
{
    public class StringScan
    {
        public List<StringVectorPoint> Data { get; set; }

        public int MinPts { get; set; }

        public double Radius { get; set; }

        public Distance.GetDistance GetDistance { get; set; }

        public long RangeRadius { get; set; } = (long)Constants.EventMaximumInterval.TotalSeconds;

        private int ClusterCount { get; set; } = 0;

        //private const int RangeRadius = 10000;

        private List<int> CurrentCluster { get; set; }

        private List<Tuple<int,int>> RangeList { get; set; }

        public void Cluster()
        {
            using (var status = StatusWrapper.NewStatus("string", this.Data.Count))
            {
                this.RangeList = TimeCalculator
                    .GetIndexRangeByTimeInterval(this.Data.Select(x => x.TimeTick).ToList(), RangeRadius);

                //Console.WriteLine(this.RangeDiameter);
                //this.StatusController.SetCurrentStatusName("DBSCAN:" + Title);
                //this.StatusController.SetTotalProgress(this.Data.Count);
                for (int i = 0; i < this.Data.Count; i++)
                {
                    if (this.Data[i].ClusterId > 0)
                    {
                        continue;
                    }

                    CurrentCluster = this.InnerClustering(i);

                    int t = 0;
                    while (t < this.CurrentCluster.Count)
                    {
                        var tl = this.InnerClustering(this.CurrentCluster[t++]);
                        if (tl.Any())
                        {
                            this.CurrentCluster.AddRange(tl);
                        }
                    }
                    //this.StatusController.SetCurrentProgress(this.CurrentCluster.Count);
                    status.PushProgress(this.CurrentCluster.Count);
                }
            }

            //this.StatusController.SetCurrentStatusName("Validation:" + Title);
            //this.StatusController.SetTotalProgress(this.Data.Count);
            this.Validation();
            //this.StatusController.SetCurrentStatusName("待命");
        }

        private List<int> InnerClustering(int index)
        {
            var neighbors = this.Data
                .GetRange(this.RangeList[index].Item1, this.RangeList[index].Item2 - this.RangeList[index].Item1)
                .AsParallel()
                //.Where(x => x.ClusterId == 0 && x.TimeTick > start && x.TimeTick < end && VectorPoint.Distance(target, x, this.GetDistance) < this.Radius)
                .Where(x => x.ClusterId == 0 && VectorPointHelper.GetDistance(this.Data[index], x) < this.Radius)
                .Select(x => x.Id)
                .ToList();

            if (neighbors.Any())
            {
                if (this.Data[index].ClusterId > 0)
                {
                    neighbors.ForEach(x =>
                    {
                        this.Data[x].ClusterId = this.Data[index].ClusterId;
                        this.Data[x].LinkingId = this.Data[index].Id;
                    });
                }
                else
                {
                    ClusterCount++;
                    this.Data[index].ClusterId = ClusterCount;
                    neighbors.ForEach(x =>
                    {
                        this.Data[x].ClusterId = ClusterCount;
                        this.Data[x].LinkingId = this.Data[index].Id;
                    });
                }
                //this.StatusController.SetCurrentProgress(neighbors.Count());

                //foreach (var item in neighbors)
                //{
                //    this.InnerClustering(item);
                //}
            }

            return neighbors;
        }

        private void Validation()
        {
            var newClusterId = 0;
            var newAssing = false;
            var group = this.Data.GroupBy(x => x.ClusterId);
            foreach (var item in group)
            {
                if (item.Count() < this.MinPts)
                {
                    newAssing = true;
                    foreach (var x in item)
                    {
                        x.ClusterId = 0;
                        x.LinkingId = 0;
                    }
                }
                else
                {
                    newClusterId++;
                    if (newAssing)
                    {
                        foreach (var x in item)
                        {
                            x.ClusterId = newClusterId;
                        }
                    }
                }
            }
        }
    }
}
