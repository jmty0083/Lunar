using LogAnalysisClustering.Misc;
using LogAnalysisLibrary.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Vectorize.StringEmbedding
{
    public class StringEmbeddingModel:IEmbeddingModel
    {
        public string GetClusteredFilename(string filename)
        {
            return Definitions.GetStringClusteredCsvFilename(filename);
        }

        public int GetIndexByWord(string word)
        {
            throw new NotImplementedException();
        }

        public string GetInflectionPointName(string filename)
        {
            return Definitions.GetStringInflectionPointFilename(filename);
        }

        public string GetKDisCurveName(string filename)
        {
            return Definitions.GetStringKDisCurveInfoFilename(filename);
        }

        public string GetModelName(string filename)
        {
            return string.Empty;
        }

        public string[] GetStringVector(KeyFeature keyFeature)
        {
            return new string[] {
                keyFeature.ThreatName,
                keyFeature.SourceIp,
                keyFeature.TargetIp,
                keyFeature.TargetPort,
                keyFeature.Protocol };
        }

        public List<double[]> GetVectors()
        {
            throw new NotImplementedException();
        }

        public string GetWordByIndex(int index)
        {
            throw new NotImplementedException();
        }

        public double[] LookupVectorByIndex(int index)
        {
            throw new NotImplementedException();
        }

        public double[] LookupVectorByWord(string word)
        {
            throw new NotImplementedException();
        }

        public void ReadModel(string filename)
        {
        }

        public void SaveModel(string filename)
        {
        }
    }
}
