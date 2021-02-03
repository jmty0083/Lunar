using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogAnalysisClustering.Vectorize.Misc.Contexts;

namespace LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model
{
    public class Ip2VecSettings
    {
        public Dictionary<GetMem, List<GetMem>> ContextRelationDict = new Dictionary<GetMem, List<GetMem>>();

        public int NeuronCount { get; set; } = 200;

        public int EpochsCount { get; set; } = 10;

        public int NegativeSampling { get; set; } = 40;

        public int MaximumParallelWorker { get; set; } = 8;

        public double LearningRate { get; set; } = .1;

        //internal bool UseRandomInitialization { get; set; } = true;

        public static Dictionary<string, List<string>> DefaultIp2VecContextsSettings { get; set; } = new Dictionary<string, List<string>>
        {
            {
                Definitions.SourceIpCN, new List<string>
                {
                    Definitions.TargetIpCN,
                    Definitions.ThreatNameCN,
                    Definitions.ProtocolCN,
                    Definitions.TargetPortCN
                }
            },
            {
                Definitions.ProtocolCN, new List<string>
                {
                    Definitions.ThreatNameCN,
                    Definitions.TargetIpCN
                }
            },
            {
                Definitions.TargetIpCN, new List<string>
                {
                    Definitions.ThreatNameCN,
                    Definitions.SourceIpCN,
                    Definitions.ProtocolCN
                }
            },
            {
                Definitions.TargetPortCN, new List<string>
                {
                    Definitions.ThreatNameCN,
                    Definitions.TargetIpCN
                }
            },
        };

        public static Ip2VecSettings Default => GetDefault();

        private static Ip2VecSettings GetDefault()
        {
            var defaultSettings = new Ip2VecSettings();
            foreach (var sentence in DefaultIp2VecContextsSettings)
            {
                var contexts = new List<GetMem>();
                foreach (var context in sentence.Value)
                {
                    contexts.Add(ContextFuncDict[context]);
                }
                defaultSettings.ContextRelationDict.Add(ContextFuncDict[sentence.Key], contexts);
            }

            return defaultSettings;
        }
    }
}
