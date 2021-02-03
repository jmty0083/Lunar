using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LogAnalysisLibrary.DataType.VectorPoint
{
    public abstract class VectorPointBase:IVectorPoint
    {
        public int Id { get; set; }

        public object[] Vector { get; set; }

        public long TimeTick { get; set; }

        [XmlIgnore]
        public int ClusterId { get; set; }

        [XmlIgnore]
        public int LinkingId { get; set; }
    }
}
