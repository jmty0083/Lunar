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
    public class KeyFeatureClusteredCategorized: KeyFeatureClustered
    {
        [DisplayName("威胁类别")]
        public string Category { get; set; }

        [DisplayName("标签")]
        public string Tag { get; set; }

        public static new KeyFeatureClusteredCategorized FromCSV(string line)
        {
            var info = line.Split(Symbols.CSVSeparator);

            return info.CreateKeyFeatureClusteredCategorized();
        }

        public override object Clone()
        {
            return base.Clone();
        }
    }
}
