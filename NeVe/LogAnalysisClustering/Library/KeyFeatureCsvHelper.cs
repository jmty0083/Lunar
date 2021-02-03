using LogAnalysisLibrary.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.DataType.Helper
{
    public static class KeyFeatureCsvHelper
    {
        private static readonly Dictionary<string, int> CsvOrdering = new Dictionary<string, int>
        {
            {nameof(KeyFeature.Timestamp), 0 },
            {nameof(KeyFeature.SourceIp), 1 },
            {nameof(KeyFeature.SourcePort), 2 },
            {nameof(KeyFeature.TargetIp), 3 },
            {nameof(KeyFeature.TargetPort), 4 },
            {nameof(KeyFeature.Protocol), 5 },
            {nameof(KeyFeature.ThreatName), 6 },
            {nameof(KeyFeature.Sid), 7 },
            {nameof(KeyFeatureClustered.Id), 8 },
            {nameof(KeyFeatureClustered.ClusterId), 9 },
            {nameof(KeyFeatureClustered.LinkingId), 10 },
            {nameof(KeyFeatureClusteredCategorized.Category), 11 },
            {nameof(KeyFeatureClusteredCategorized.Tag), 12 },
            {nameof(KeyFeatureOfRandomEvent.EventId), 13 },
            {nameof(KeyFeatureOfRandomEvent.RandomType), 14 },
        };

        public static KeyFeature CreateKeyFeature(this string[] line)
        {
            return new KeyFeature
            {
                Timestamp = line[CsvOrdering[nameof(KeyFeature.Timestamp)]],
                SourceIp = line[CsvOrdering[nameof(KeyFeature.SourceIp)]],
                SourcePort = line[CsvOrdering[nameof(KeyFeature.SourcePort)]],
                TargetIp = line[CsvOrdering[nameof(KeyFeature.TargetIp)]],
                TargetPort = line[CsvOrdering[nameof(KeyFeature.TargetPort)]],
                Protocol = line[CsvOrdering[nameof(KeyFeature.Protocol)]],
                ThreatName = line[CsvOrdering[nameof(KeyFeature.ThreatName)]],
                Sid = line.Length >= 8 ? (line[CsvOrdering[nameof(KeyFeature.Sid)]] == "null" ? null : line[CsvOrdering[nameof(KeyFeature.Sid)]]) : "null" ,
            };
        }

        public static string GetCsv(this KeyFeature item)
        {
            var array = new string[CsvOrdering.Count];
            array[CsvOrdering[nameof(KeyFeature.Timestamp)]] = item.Timestamp.ToString();
            array[CsvOrdering[nameof(KeyFeature.SourceIp)]] = item.SourceIp.ToString();
            array[CsvOrdering[nameof(KeyFeature.SourcePort)]] = item.SourcePort.ToString();
            array[CsvOrdering[nameof(KeyFeature.TargetIp)]] = item.TargetIp.ToString();
            array[CsvOrdering[nameof(KeyFeature.TargetPort)]] = item.TargetPort.ToString();
            array[CsvOrdering[nameof(KeyFeature.Protocol)]] = item.Protocol.ToString();
            array[CsvOrdering[nameof(KeyFeature.ThreatName)]] = item.ThreatName.ToString();
            array[CsvOrdering[nameof(KeyFeature.Sid)]] = item.Sid.ToString();

            return string.Join(Symbols.CSVSeparator.ToString(), array);
        }

        public static KeyFeatureClustered CreateKeyFeatureClustered(this string[] line)
        {
            return new KeyFeatureClustered
            {
                Timestamp = line[CsvOrdering[nameof(KeyFeature.Timestamp)]],
                SourceIp = line[CsvOrdering[nameof(KeyFeature.SourceIp)]],
                SourcePort = line[CsvOrdering[nameof(KeyFeature.SourcePort)]],
                TargetIp = line[CsvOrdering[nameof(KeyFeature.TargetIp)]],
                TargetPort = line[CsvOrdering[nameof(KeyFeature.TargetPort)]],
                Protocol = line[CsvOrdering[nameof(KeyFeature.Protocol)]],
                ThreatName = line[CsvOrdering[nameof(KeyFeature.ThreatName)]],
                Sid = line[CsvOrdering[nameof(KeyFeature.Sid)]],
                Id = int.Parse(line[CsvOrdering[nameof(KeyFeatureClustered.Id)]]),
                ClusterId = int.Parse(line[CsvOrdering[nameof(KeyFeatureClustered.ClusterId)]]),
                LinkingId = int.Parse(line[CsvOrdering[nameof(KeyFeatureClustered.LinkingId)]]),
            };
        }

        public static string GetCsv(this KeyFeatureClustered item)
        {
            var array = new string[CsvOrdering.Count];
            array[CsvOrdering[nameof(KeyFeature.Timestamp)]] = item.Timestamp.ToString();
            array[CsvOrdering[nameof(KeyFeature.SourceIp)]] = item.SourceIp.ToString();
            array[CsvOrdering[nameof(KeyFeature.SourcePort)]] = item.SourcePort.ToString();
            array[CsvOrdering[nameof(KeyFeature.TargetIp)]] = item.TargetIp.ToString();
            array[CsvOrdering[nameof(KeyFeature.TargetPort)]] = item.TargetPort.ToString();
            array[CsvOrdering[nameof(KeyFeature.Protocol)]] = item.Protocol.ToString();
            array[CsvOrdering[nameof(KeyFeature.ThreatName)]] = item.ThreatName.ToString();
            array[CsvOrdering[nameof(KeyFeature.Sid)]] = item.Sid.ToString();
            array[CsvOrdering[nameof(KeyFeatureClustered.Id)]] = item.Id.ToString();
            array[CsvOrdering[nameof(KeyFeatureClustered.ClusterId)]] = item.ClusterId.ToString();
            array[CsvOrdering[nameof(KeyFeatureClustered.LinkingId)]] = item.LinkingId.ToString();

            return string.Join(Symbols.CSVSeparator.ToString(), array);
        }

        public static KeyFeatureClusteredCategorized CreateKeyFeatureClusteredCategorized(this string[] line)
        {
            return new KeyFeatureClusteredCategorized
            {
                Timestamp = line[CsvOrdering[nameof(KeyFeature.Timestamp)]],
                SourceIp = line[CsvOrdering[nameof(KeyFeature.SourceIp)]],
                SourcePort = line[CsvOrdering[nameof(KeyFeature.SourcePort)]],
                TargetIp = line[CsvOrdering[nameof(KeyFeature.TargetIp)]],
                TargetPort = line[CsvOrdering[nameof(KeyFeature.TargetPort)]],
                Protocol = line[CsvOrdering[nameof(KeyFeature.Protocol)]],
                ThreatName = line[CsvOrdering[nameof(KeyFeature.ThreatName)]],
                Sid = line[CsvOrdering[nameof(KeyFeature.Sid)]],
                Id = int.Parse(line[CsvOrdering[nameof(KeyFeatureClustered.Id)]]),
                ClusterId = int.Parse(line[CsvOrdering[nameof(KeyFeatureClustered.ClusterId)]]),
                LinkingId = int.Parse(line[CsvOrdering[nameof(KeyFeatureClustered.LinkingId)]]),
                Category = line[CsvOrdering[nameof(KeyFeatureClusteredCategorized.Category)]],
                Tag = line[CsvOrdering[nameof(KeyFeatureClusteredCategorized.Tag)]],
            };
        }

        public static KeyFeatureOfRandomEvent CreateKeyFeatureOfRandomEvent(this string[] line)
        {
            return new KeyFeatureOfRandomEvent
            {
                Timestamp = line[CsvOrdering[nameof(KeyFeature.Timestamp)]],
                SourceIp = line[CsvOrdering[nameof(KeyFeature.SourceIp)]],
                SourcePort = line[CsvOrdering[nameof(KeyFeature.SourcePort)]],
                TargetIp = line[CsvOrdering[nameof(KeyFeature.TargetIp)]],
                TargetPort = line[CsvOrdering[nameof(KeyFeature.TargetPort)]],
                Protocol = line[CsvOrdering[nameof(KeyFeature.Protocol)]],
                ThreatName = line[CsvOrdering[nameof(KeyFeature.ThreatName)]],
                Sid = line.Length >= 8 ? (line[CsvOrdering[nameof(KeyFeature.Sid)]] == "null" ? null : line[CsvOrdering[nameof(KeyFeature.Sid)]]) : "null",
                EventId = line[CsvOrdering[nameof(KeyFeatureOfRandomEvent.EventId)]],
                RandomType = line[CsvOrdering[nameof(KeyFeatureOfRandomEvent.RandomType)]]
            };
        }

        public static string GetCsv(this KeyFeatureOfRandomEvent item)
        {
            var array = new string[CsvOrdering.Count];
            array[CsvOrdering[nameof(KeyFeature.Timestamp)]] = item.Timestamp.ToString();
            array[CsvOrdering[nameof(KeyFeature.SourceIp)]] = item.SourceIp.ToString();
            array[CsvOrdering[nameof(KeyFeature.SourcePort)]] = item.SourcePort.ToString();
            array[CsvOrdering[nameof(KeyFeature.TargetIp)]] = item.TargetIp.ToString();
            array[CsvOrdering[nameof(KeyFeature.TargetPort)]] = item.TargetPort.ToString();
            array[CsvOrdering[nameof(KeyFeature.Protocol)]] = item.Protocol.ToString();
            array[CsvOrdering[nameof(KeyFeature.ThreatName)]] = item.ThreatName.ToString();
            array[CsvOrdering[nameof(KeyFeature.Sid)]] = item.Sid.ToString();
            array[CsvOrdering[nameof(KeyFeatureOfRandomEvent.EventId)]] = item.EventId.ToString();
            array[CsvOrdering[nameof(KeyFeatureOfRandomEvent.RandomType)]] = item.RandomType.ToString();

            return string.Join(Symbols.CSVSeparator.ToString(), array);
        }

    }
}
