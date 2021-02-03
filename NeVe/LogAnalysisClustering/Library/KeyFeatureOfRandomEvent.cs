using LogAnalysisLibrary.DataType.Helper;
using LogAnalysisLibrary.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.DataType
{
    public class KeyFeatureOfRandomEvent: KeyFeature
    {
        [DisplayName("事件ID")]
        public string EventId { get; set; }

        [DisplayName("随机类型")]
        public string RandomType { get; set; }

        public KeyFeatureOfRandomEvent(KeyFeature keyFeature)
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

        public KeyFeatureOfRandomEvent() { }

        public static KeyFeatureOfRandomEvent FromCSV(string line)
        {
            var info = line.Split(Symbols.CSVSeparator);

            return info.CreateKeyFeatureOfRandomEvent();
        }
    }
}
