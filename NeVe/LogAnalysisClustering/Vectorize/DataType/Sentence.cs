using LogAnalysisLibrary.DataType;
using System.Collections.Generic;

namespace LogAnalysisClustering.Vectorize.DataType
{
    public class Sentence : KeyFeature
    {
        //private new string Sid { get; set; }
        //private new string Timestamp { get; set; }

        public Sentence() { }

        public Sentence(KeyFeature keyFeature)
        {
            ThreatName = keyFeature.ThreatName;
            SourceIp = keyFeature.SourceIp;
            SourcePort = keyFeature.SourcePort;
            TargetIp = keyFeature.TargetIp;
            TargetPort = keyFeature.TargetPort;
            Protocol = keyFeature.Protocol;
            Timestamp = keyFeature.Timestamp;
            Sid = keyFeature.Sid;
        }

        public override bool Equals(object obj)
        {
            return obj is Sentence sentence &&
                   ThreatName == sentence.ThreatName &&
                   SourceIp == sentence.SourceIp &&
                   SourcePort == sentence.SourcePort &&
                   TargetIp == sentence.TargetIp &&
                   TargetPort == sentence.TargetPort &&
                   Protocol == sentence.Protocol;
        }

        public override int GetHashCode()
        {
            var hashCode = 509787655;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThreatName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourceIp);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourcePort);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TargetIp);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TargetPort);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Protocol);
            return hashCode;
        }
    }

}
