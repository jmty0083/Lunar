using Accord.MachineLearning.Clustering;
using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize;
using LogAnalysisLibrary.DataType;
using LogAnalysisLibrary.DataType.VectorPoint;
using LogAnalysisLibrary.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Measure
{
    public class TSNEVisualization
    {
        public IEmbeddingModel Model { get; set; }

        public List<KeyFeatureClusteredCategorized> Data { get; set; }

        private TSNE TSNE { get; set; } = new TSNE
        {
            NumberOfOutputs = 2,
            Perplexity = 50,
            Theta = .3
        };

        public void toSNE(string filename)
        {
            var csv = new Csv();
            var points = this.Data.Select(x => ((DoubleVectorPoint)x.ToVectorPoint(Model,0)).Vector).ToArray();
            var tSNEpoints = this.TSNE.Transform(points);

            var a = tSNEpoints
                .Select((x, i) => x.Select(y => y.ToString("R"))
                    .Append(this.Data[i].Category.Split(Symbols.IdsRuleSeparator).FirstOrDefault()));
            csv.AppendLines(a);

            csv.SaveCsvFile(filename);
        }
    }
}
