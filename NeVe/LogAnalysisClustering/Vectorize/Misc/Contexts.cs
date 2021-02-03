using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Vectorize.Misc
{
    public static class Contexts
    {
        public delegate string GetMem(Sentence s);

        public static Dictionary<string, GetMem> ContextFuncDict { get; set; }
            = new Dictionary<string, GetMem>
        {
            { Definitions.SourceIpCN, x => x.SourceIp },
            { Definitions.TargetIpCN, x => x.TargetIp },
            { Definitions.SourcePortCN, x => x.SourcePort },
            { Definitions.TargetPortCN, x => x.TargetPort },
            { Definitions.ThreatNameCN, x => x.ThreatName },
            { Definitions.ProtocolCN, x => x.Protocol },
        };

    }
}
