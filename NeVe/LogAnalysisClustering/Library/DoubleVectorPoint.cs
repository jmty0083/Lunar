using LogAnalysisLibrary.Algorithms.Mathematics;
using System;
using System.Xml.Serialization;

namespace LogAnalysisLibrary.DataType.VectorPoint
{
    public class DoubleVectorPoint : VectorPointBase
    {
        public new double[] Vector { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:<{1}>", Id, string.Join(",", Vector));
        }
    }
}
