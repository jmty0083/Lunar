using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LogAnalysisLibrary.DataType
{
    public class KeyFeature: ICloneable
    {
        [DisplayName("SID")]
        public string Sid { get; set; }

        [DisplayName("攻击名称")]
        public string ThreatName { get; set; }

        [DisplayName("源IP")]
        public string SourceIp { get; set; }

        [DisplayName("源端口")]
        public string SourcePort { get; set; }

        [DisplayName("目标IP")]
        public string TargetIp { get; set; }

        [DisplayName("目标端口")]
        public string TargetPort { get; set; }

        [DisplayName("协议")]
        public string Protocol { get; set; }

        [DisplayName("时间")]
        public string Timestamp { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [Flags]
    public enum KeyFeatureComparerSelector
    {
        SourceIp = 1,
        TargetIp = 2,
        SourcePort = 4,
        TargetPort = 8,
        ThreatName = 16,
        Protocol = 32
    }

    public class KeyFeatureSameType : IEqualityComparer<KeyFeature>
    {
        private delegate string SelectComparerFunc(KeyFeature x);

        private static readonly SelectComparerFunc SourceIpComparer = x => x.SourceIp;
        private static readonly SelectComparerFunc TargetIpComparer = x => x.TargetIp; 
        private static readonly SelectComparerFunc SourcePortComparer = x => x.SourcePort;
        private static readonly SelectComparerFunc TargetPortComparer = x => x.TargetPort;
        private static readonly SelectComparerFunc ThreatNameComparer = x => x.ThreatName; 
        private static readonly SelectComparerFunc ProtocolComparer = x => x.Protocol;

        private List<SelectComparerFunc> Comparers { get; set; }

        public KeyFeatureSameType()
        {
            Comparers = new List<SelectComparerFunc>
            {
                SourceIpComparer,
                SourcePortComparer,
                TargetIpComparer,
                TargetPortComparer,
                ThreatNameComparer,
                ProtocolComparer
            };
        }

        public KeyFeatureSameType(KeyFeatureComparerSelector selectors)
        {
            Comparers = new List<SelectComparerFunc>();
            if (selectors.HasFlag(KeyFeatureComparerSelector.SourceIp))
            {
                Comparers.Add(SourceIpComparer);
            }
            if (selectors.HasFlag(KeyFeatureComparerSelector.TargetIp))
            {
                Comparers.Add(TargetIpComparer);
            }
            if (selectors.HasFlag(KeyFeatureComparerSelector.SourcePort))
            {
                Comparers.Add(SourcePortComparer);
            }
            if (selectors.HasFlag(KeyFeatureComparerSelector.TargetPort))
            {
                Comparers.Add(TargetPortComparer);
            }
            if (selectors.HasFlag(KeyFeatureComparerSelector.ThreatName))
            {
                Comparers.Add(ThreatNameComparer);
            }
            if (selectors.HasFlag(KeyFeatureComparerSelector.Protocol))
            {
                Comparers.Add(ProtocolComparer);
            }
            if (Comparers.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Unknown comparer settings");
            }
        }

        public bool Equals(KeyFeature x, KeyFeature y)
        {
            if (x == y)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else
            {
                foreach (var cmp in Comparers)
                {
                    if (cmp(x) != cmp(y))
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        public int GetHashCode(KeyFeature obj)
        {
            unchecked
            {
                var hashCode = 1810473954;
                foreach (var cmp in this.Comparers)
                {
                    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(cmp(obj));
                }
                return hashCode;
            }
        }
    }
}
