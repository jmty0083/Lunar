using LogAnalysisClustering.Misc;
using LogAnalysisLibrary.Algorithms;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LogAnalysisClustering.Vectorize.NeVeEmbedding.Model
{
    public class NeVeModel : IEmbeddingModel
    {
        public Dictionary<string, int> Word2Index { get; set; }

        internal List<Vector<double>> MainVectors { get; set; } = new List<Vector<double>>();

        internal List<Vector<double>> ContextVectors { get; set; } = new List<Vector<double>>();

        public List<double[]> GetVectors()
        {
            return GetVectorsHelper().ToList();
        }

        public double[] LookupVectorByWord(string word)
        {
            return ((MainVectors[Word2Index[word]] + ContextVectors[Word2Index[word]]) / 2).ToArray();
        }

        public double[] LookupVectorByIndex(int index)
        {
            return ((MainVectors[index] + ContextVectors[index]) / 2).ToArray();
        }

        public int GetIndexByWord(string word)
        {
            return Word2Index[word];
        }

        public string GetWordByIndex(int index)
        {
            return Word2Index.FirstOrDefault(x => x.Value == index).Key;
        }

        public void SaveModel(string filename)
        {
            var data = PreSerializer(this);

            using (StreamWriter file = new StreamWriter(filename, false))
            {
                data.MainVectorString.ForEach(x => file.WriteLine(x));
                file.WriteLine();

                data.ContextVectorString.ForEach(x => file.WriteLine(x));
                file.WriteLine();

                file.WriteLine(data.Indexer);
                //file.WriteLine(data.MainVectorString);
                //file.WriteLine(data.ContextVectorString);
                //file.WriteLine(data.Indexer);
                //file.WriteLine(DataStructures.Serialization(data, data.GetType()));
            }
            //file.WriteLine(DataStructures.Serialization(data, data.GetType()));
        }

        public void ReadModel(string filename)
        {
            //var contents = File.ReadAllText(filename);

            var data = new ModelSaver
            {
                MainVectorString = new List<string>(),
                ContextVectorString = new List<string>(),
                //Indexer = contents[2],
            };//

            var index = 0;
            foreach (var item in File.ReadLines(filename))
            {
                if (string.IsNullOrEmpty(item))
                {
                    index++;
                }
                else
                {
                    switch (index)
                    {
                        case 0:
                            data.MainVectorString.Add(item);
                            break;
                        case 1:
                            data.ContextVectorString.Add(item);
                            break;
                        default:
                            data.Indexer = item;
                            break;
                    }
                }

            }

            //var data = (ModelSaver)DataStructures.Deserialization(contents, typeof(ModelSaver));

            SuDeserializer(data, this);
            //this.WordCount = this.Word2Index.Count();
            //this.Ip2VecSettings.NeuronCount = data.Y;
        }

        public static ModelSaver PreSerializer(NeVeModel model)
        {
            var mainVectorString = model.MainVectors.Select(x => string.Join(",", x.Select(y => y.ToString("R")))).ToList();
            var contextVectorString = model.ContextVectors.Select(x => string.Join(",", x.Select(y => y.ToString("R")))).ToList();
            var words = string.Join(",", model.Word2Index.OrderBy(x => x.Value).Select(x => x.Key));

            return new ModelSaver
            {
                Indexer = words,
                MainVectorString = mainVectorString,
                ContextVectorString = contextVectorString
            };

        }

        private static void SuDeserializer(ModelSaver data, NeVeModel model)
        {
            model.MainVectors = data.MainVectorString.Select(x => Vector<double>.Build.Dense(Array.ConvertAll(x.Split(','), double.Parse))).ToList();
            model.ContextVectors = data.ContextVectorString.Select(x => Vector<double>.Build.Dense(Array.ConvertAll(x.Split(','), double.Parse))).ToList();
            var index = 0;
            model.Word2Index = data.Indexer.Split(',').ToDictionary(x => x, x => index++);
        }

        private IEnumerable<double[]> GetVectorsHelper()
        {
            for (int i = 0; i < Word2Index.Count; i++)
            {
                yield return LookupVectorByIndex(i);
            }
        }

        public string GetModelName(string filename)
        {
            return Definitions.GetNeVeModelFilename(filename);
        }

        public string GetKDisCurveName(string filename)
        {
            return Definitions.GetGloVeKDisCurveInfoFilename(filename);
        }

        public string GetInflectionPointName(string filename)
        {
            return Definitions.GetGloVeInflectionPointFilename(filename);
        }

        public string GetClusteredFilename(string filename)
        {
            return Definitions.GetGloVeClusteredCsvFilename(filename);
        }
    }

    public class ModelSaver
    {
        public List<string> MainVectorString { get; set; }

        public List<string> ContextVectorString { get; set; }

        public string Indexer { get; set; }
    }
}
