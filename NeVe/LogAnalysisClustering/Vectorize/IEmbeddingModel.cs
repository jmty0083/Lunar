using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Vectorize
{
    public interface IEmbeddingModel
    {
        List<double[]> GetVectors();

        double[] LookupVectorByWord(string word);

        double[] LookupVectorByIndex(int index);

        int GetIndexByWord(string word);

        string GetWordByIndex(int index);

        void SaveModel(string filename);

        void ReadModel(string filename);

        string GetModelName(string filename);

        string GetKDisCurveName(string filename);

        string GetInflectionPointName(string filename);

        string GetClusteredFilename(string filename);
    }
}
