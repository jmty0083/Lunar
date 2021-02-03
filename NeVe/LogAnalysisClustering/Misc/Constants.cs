using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Misc
{
    public static class Constants
    {
        public const string DefaultLoggingDirectory = @"E:\MarsRelease\EventExtractionExam\Ip2VecLog_Figure\";
        public const string DefaultDebugLoggingDirectory = @"E:\EventExtractionExam\Debug\";
        public const string DefaultCsvBrowsingPath = @"E:\EventExtractionExam\CSV\";

        public const string DefaultIp2VecModelSuffix = @".model_ip2vec";
        public const string DefaultNeVeModelSuffix = @".model_glove";

        public const string DefaultIp2VecKDisCurveInfoSuffix = @"_ip2vec.kds";
        public const string DefaultIp2VecInflectionPointSuffix = @"_ip2vec.ipp";
        public const string DefaultNeVeKDisCurveInfoSuffix = @"_glove.kds";
        public const string DefaultNeVeInflectionPointSuffix = @"_glove.ipp";
        public const string DefaultStringKDisCurveInfoSuffix = @"_string.kds";
        public const string DefaultStringInflectionPointSuffix = @"_string.ipp";

        public const string DefaultIp2VecClusteredCSVSuffix = @"_ip2vec_clustered.csv";
        public const string DefaultNeVeClusteredCSVSuffix = @"_glove_clustered.csv";
        public const string DefaultStringClusteredCSVSuffix = @"_string_clustered.csv";

        public const int DebugSeed = 2217;

        public const double StringSimilarityThreshold = 1.0d;

        public static readonly TimeSpan EventMaximumInterval = TimeSpan.FromMinutes(20);
    }
}
