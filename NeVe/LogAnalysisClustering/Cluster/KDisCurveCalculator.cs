using LogAnalysisClustering.DataType;
using LogAnalysisClustering.Misc;
using LogAnalysisLibrary.Algorithms;
using LogAnalysisLibrary.Algorithms.Mathematics;
using LogAnalysisLibrary.DataType.Helper;
using LogAnalysisLibrary.DataType.VectorPoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Serialization;

namespace LogAnalysisClustering.Cluster
{
    public class KDisCurveCalculator
    {
        public List<VectorPointBase> Data { get; set; }

        public List<List<NearestNeighbor>> Neighbors { get; private set; } = new List<List<NearestNeighbor>>();

        public List<double> KDisCurve { get; private set; }

        public List<InflectionPoint> InflectionPointList { get; private set; }

        private const int withTop = 4;

        private static readonly TimeSpan kDisDiameter = Constants.EventMaximumInterval;

        public Distance.GetDistance MathDistance { get; set; } = Distance.Manhattan;

        public void Calculate()
        {
            this.BuildNeighbors();
            this.GetKDisCurve();
            this.GetInflectionPointList(this.KDisCurve);
        }

        // new Chart may result in failure of StatusController worker on RunWorkerCompleted
        public Chart GetKDisCurveFigure()
        {
            var chart = new Chart()
            {
                Height = 600,
                Width = 800
            };
            ChartArea chartArea = new ChartArea
            {
                AxisX = new Axis()
                {
                    IsLogarithmic = true,
                    Minimum = 1,
                },
                AxisY = new Axis(),
                //{
                //    IsLogarithmic = true,
                //    Minimum = 1
                //},
                Name = "Default"
            };
            chart.ChartAreas.Add(chartArea);

            var series = new Series
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = false,
                IsVisibleInLegend = false,
            };
            //series.LabelFormat = "{0:0.####}";
            chart.Series.Add(series);
            //this.ChartBox.Series.Add(series);

            var legendname = string.Format("{0}-dis figure", withTop);
            chart.Legends.Add(new Legend(legendname) { DockedToChartArea = "Default", Name = legendname });
            series.Legend = legendname;
            series.LegendText = legendname;

            for (int i = 0; i < this.KDisCurve.Count; i++)
            {//*
                series.Points.AddXY(i + 1, KDisCurve[i]);
            }

            this.AddInflectionPointLine(chart);
            return chart;
        }

        public void SaveNearestNeighborList(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename, false))
            {
                foreach (var item in this.Neighbors)
                {
                    file.WriteLine((item.Sum(t => t.Distance) / item.Count) + ":" + string.Join("|", item));
                }
            }
        }

        public void SaveInflectionPointList(string filename)
        {
            XmlSerializer xsSubmit = new XmlSerializer(this.InflectionPointList.GetType());

            using (StreamWriter file = new StreamWriter(filename, false))
            {
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xsSubmit.Serialize(writer, this.InflectionPointList);
                        file.WriteLine(sww.ToString());
                    }
                }
            }
        }
        
        public static List<InflectionPoint> ReadInflectionPointList(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<InflectionPoint>));
            using (TextReader reader = new StringReader(File.ReadAllText(filename)))
            {
                return (List<InflectionPoint>)serializer.Deserialize(reader);
            }
        }

        private void BuildNeighbors()
        {
            using (var status = StatusWrapper.NewStatus("Building Neighbors", this.Data.Count))
            {
                var heap = new EasyHeapSmallFixedSize<NearestNeighbor>(HeapType.MinHeap, withTop, x => x.Distance);

                var rangeList = TimeCalculator.GetIndexRangeByTimeInterval(this.Data.Select(x => x.TimeTick).ToList(), Convert.ToInt64(kDisDiameter.TotalSeconds));

                for (int i = 0; i < this.Data.Count; i++)
                {
                    this.Data.GetRange(rangeList[i].Item1, rangeList[i].Item2 - rangeList[i].Item1)
                        .ForEach(x =>
                        {
                            var g = VectorPointHelper.GetDistance(this.Data[i], x, MathDistance);
                            if (g > 0)
                            {
                                heap.Push(new NearestNeighbor { Distance = g, NeighborIndex = x.Id });
                            }
                        });
                    
                    this.Neighbors.Add(heap.GetList().ToList());
                    status.PushProgress();
                }
            }
        }
        
        private void GetKDisCurve()
        {
            if (this.Neighbors.Count < 1)
            {
                throw new InvalidOperationException("KDis Curves unprepared!");
            }

            this.KDisCurve = this.Neighbors
                .Select(x => x.Sum(t => t.Distance) / x.Count)
                //.Distinct()
                .OrderByDescending(x => x)
                .ToList();
        }

        private void GetInflectionPointList(List<double> list)
        {
            //var diff = new Dictionary<double, InflectionPoint>();
            //for (int i = 1; i < list.Count - 1; i++)
            //{
            //    var x = Math.Abs(list[i + 1] - list[i - 1]);
            //    if (diff.ContainsKey(list[i]))
            //    {
            //        var item = diff[list[i]];
            //        item.Slope = Math.Min(item.Slope, x);
            //        item.Count++;
            //    }
            //    else
            //    {
            //        diff.Add(list[i], new InflectionPoint { Slope = x, KDisValue = list[i] });
            //    }
            //}

            //var points = diff.OrderBy(x => x.Key).Take(withTop);
            //this.InflectionPoint = points.First().Value;
            //return this.InflectionPoint;

            var diff = new Dictionary<double, InflectionPoint>();
            for (int i = 1; i < list.Count - 1; i++)
            {
                var x = Math.Abs(list[i + 1] - list[i - 1]);
                if (diff.ContainsKey(list[i]))
                {
                    var item = diff[list[i]];
                    item.Slope = Math.Min(item.Slope, x);
                    item.Count++;
                }
                else
                {
                    diff.Add(list[i], new InflectionPoint { Slope = x, KDisValue = list[i] });
                }
            }


            var temp2 = diff.OrderByDescending(x => x.Value.Count)
                .Take(withTop)
                .Select(x => x.Value)
                .OrderBy(x => x.KDisValue)
                .OrderBy(x => x.Slope)
                .ToList();
            //var take = temp2.First();

            this.InflectionPointList = temp2;

            //return take;
        }

        private void AddInflectionPointLine(Chart chart)
        {
            foreach (var item in this.InflectionPointList)
            {
                if (this.InflectionPointList != null)
                {
                    var strip = new Series
                    {
                        ChartType = SeriesChartType.Line,
                        IsValueShownAsLabel = true,
                        IsVisibleInLegend = false,
                        LabelFormat = "{0:0.###}",
                        Color = Color.Red
                    };
                    chart.Series.Add(strip);
                    strip.Points.AddXY(1, item.KDisValue);
                    strip.Points.AddXY(this.KDisCurve.Count - 1, item.KDisValue);
                }
            }            
        }
    }

    public class InflectionPoint
    {
        public double Slope { get; set; } = double.NaN;

        public int Count { get; set; } = 1;

        public double KDisValue { get; set; } = 0;

        //public void Save(string filename)
        //{
        //    XmlSerializer xsSubmit = new XmlSerializer(this.GetType());

        //    using (StreamWriter file = new StreamWriter(filename, false))
        //    {
        //        using (var sww = new StringWriter())
        //        {
        //            using (XmlWriter writer = XmlWriter.Create(sww))
        //            {
        //                xsSubmit.Serialize(writer, this);
        //                file.WriteLine(sww.ToString());
        //            }
        //        }
        //    }
        //}

        //public static InflectionPoint Read(string filename)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(InflectionPoint));
        //    using (TextReader reader = new StringReader(File.ReadAllText(filename)))
        //    {
        //        return (InflectionPoint)serializer.Deserialize(reader);
        //    }
        //}
    }

    public class NearestNeighbor : IComparable<NearestNeighbor>
    {
        public double Distance { get; set; }

        public int NeighborIndex { get; set; }

        public int CompareTo(NearestNeighbor other)
        {
            return this.Distance.CompareTo(other.Distance);
        }

        public override string ToString()
        {
            return string.Format("[{0}]{1}", this.Distance, this.NeighborIndex);
        }
    }
}
