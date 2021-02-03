using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogAnalysisClustering.Vectorize.Misc.Contexts;

namespace LogAnalysisClustering.Vectorize.NeVeEmbedding.Model
{
    public class NeVeSettings
    {
        public Dictionary<GetMem, Dictionary<GetMem, int>> ContextRelationDistanceDict = new Dictionary<GetMem, Dictionary<GetMem, int>>();

        public int VectorSize { get; set; } = 300;

        public double LearningRate { get; set; } = .1;

        public int EpochsCount { get; set; } = 10;

        public int XMax { get; set; } = 10000;

        public int MaximumParallelWorker { get; set; } = 8;

        public static Dictionary<string, Dictionary<string, int>> DefaultNeVeContextsSettings { get; set; } = new Dictionary<string, Dictionary<string, int>>
        {
            //{
            //    Definitions.ThreatNameCN, new Dictionary<string,int>
            //    {
            //        { Definitions.SourceIpCN, 1 },
            //        { Definitions.TargetIpCN, 1 },
            //        { Definitions.ProtocolCN, 2 },
            //        { Definitions.TargetPortCN, 2 },
            //    }
            //},
            {
                Definitions.SourceIpCN, new Dictionary<string,int>
                {
                    { Definitions.TargetIpCN, 1 },
                    { Definitions.ThreatNameCN, 1 },
                    { Definitions.ProtocolCN, 2 },
                    { Definitions.TargetPortCN, 2 },
                }
            },
            {
                Definitions.ProtocolCN, new Dictionary<string,int>
                {
                    { Definitions.ThreatNameCN, 1 },
                    { Definitions.TargetIpCN, 2 }
                }
            },
            {
                Definitions.TargetIpCN, new Dictionary<string,int>
                {
                    { Definitions.ThreatNameCN, 1 },
                    { Definitions.SourceIpCN, 1 },
                    { Definitions.ProtocolCN, 2 }
                }
            },
            {
                Definitions.TargetPortCN, new Dictionary<string,int>
                {
                    { Definitions.ThreatNameCN, 2 },
                    { Definitions.TargetIpCN, 1 }
                }
            },
        };

        //public static Dictionary<string, GetMem> ContextFuncDict { get; set; }
        //    = new Dictionary<string, GetMem>
        //{
        //    { Definitions.SourceIpCN, x => x.SourceIp },
        //    { Definitions.TargetIpCN, x => x.TargetIp },
        //    { Definitions.SourcePortCN, x => x.SourcePort },
        //    { Definitions.TargetPortCN, x => x.TargetPort },
        //    { Definitions.ThreatNameCN, x => x.ThreatName },
        //    { Definitions.ProtocolCN, x => x.Protocol },
        //};

        public static NeVeSettings Default => GetDefault();

        private static NeVeSettings GetDefault()
        {
            var defaultSettings = new NeVeSettings
            {
                ContextRelationDistanceDict = FromContext2Relation(DefaultNeVeContextsSettings)
            };
            return defaultSettings;
        }

        public static Dictionary<GetMem, Dictionary<GetMem, int>> FromContext2Relation(Dictionary<string, Dictionary<string, int>> contextdict)
        {
            var relations = new Dictionary<GetMem, Dictionary<GetMem, int>>();
            foreach (var sentence in contextdict)
            {
                var contexts = new Dictionary<GetMem, int>();
                foreach (var context in sentence.Value)
                {
                    contexts.Add(ContextFuncDict[context.Key], context.Value);
                }
                relations.Add(ContextFuncDict[sentence.Key], contexts);
            }

            return relations;
        }
    }
}
