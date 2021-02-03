using LogAnalysisLibrary.DataType.Helper;
using LogAnalysisLibrary.Systems;
using System.ComponentModel;
using System.Text;

namespace LogAnalysisLibrary.DataType
{
    public class KeyFeatureClustered : KeyFeature
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("聚类ID")]
        public int ClusterId { get; set; }

        [DisplayName("关联源ID")]
        public int LinkingId { get; set; }

        public string CSV => this.GetCsv();


        public KeyFeatureClustered(KeyFeature keyFeature)
        {
            this.Protocol = keyFeature.Protocol;
            this.Sid = keyFeature.Sid;
            this.SourceIp = keyFeature.SourceIp;
            this.SourcePort = keyFeature.SourcePort;
            this.TargetIp = keyFeature.TargetIp;
            this.TargetPort = keyFeature.TargetPort;
            this.Timestamp = keyFeature.Timestamp;
            this.ThreatName = keyFeature.ThreatName;
        }

        public KeyFeatureClustered(){ }

        public static KeyFeatureClustered FromCSV(string line)
        {
            var info = line.Split(Symbols.CSVSeparator);

            return info.CreateKeyFeatureClustered();
        }
    }
}
