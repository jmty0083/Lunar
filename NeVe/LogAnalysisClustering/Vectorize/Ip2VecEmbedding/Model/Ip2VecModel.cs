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

namespace LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model
{
    public class Ip2VecModel : IEmbeddingModel
    {
        internal Dictionary<string, int> Word2Index { get; set; } = new Dictionary<string, int>();

        internal Dictionary<int, string> Index2Word { get; set; } = new Dictionary<int, string>();

        internal Matrix<double> W1 { get; set; }

        internal Matrix<double> W2 { get; set; }

        internal List<int> Frequency { get; set; } = new List<int>();

        public List<double[]> GetVectors()
        {
            return this.W1.ToRowArrays().ToList();
        }

        public double[] LookupVectorByWord(string word)
        {
            return this.W1.Row(this.Word2Index[word]).ToArray();
        }

        public double[] LookupVectorByIndex(int index)
        {
            return this.W1.Row(index).ToArray();
        }

        public int GetIndexByWord(string word)
        {
            return this.Word2Index[word];
        }

        public string GetWordByIndex(int index)
        {
            return this.Index2Word[index];
        }

        public void PrintToFile(string filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, false))
            {
                file.WriteLine("Dictionary:");
                foreach (var item in this.Index2Word)
                {
                    file.WriteLine(item.Key + " - " + item.Value);
                }
                file.WriteLine("Frequencies:");
                file.WriteLine(string.Join(",", this.Frequency));
                file.WriteLine("W1:");
                foreach (var item in this.W1.ToRowArrays())
                {
                    file.WriteLine(string.Join(" ", item));
                }
                file.WriteLine("W2:");
                foreach (var item in this.W2.ToRowArrays())
                {
                    file.WriteLine(string.Join(" ", item));
                }
            }
        }

        public void SaveModel(string filename)
        {
            var data = PreSerializer(this);

            using (StreamWriter file = new StreamWriter(filename, false))
            {
                file.WriteLine(DataStructures.Serialization(data, data.GetType()));
            }

            Console.WriteLine("Finish writing to {0}", filename);
        }

        public void ReadModel(string filename)
        {
            string contents = File.ReadAllText(filename);
            var data = (ModelSaver)DataStructures.Deserialization(contents, typeof(ModelSaver));

            Ip2VecModel.SuDeserializer(data, this);
            //this.WordCount = this.Word2Index.Count();
            //this.Ip2VecSettings.NeuronCount = data.Y;
        }

        private static ModelSaver PreSerializer(Ip2VecModel ipNumerizerByIp2Vec)
        {
            var w1string = string.Join(",", ipNumerizerByIp2Vec.W1.ToColumnMajorArray().Select(x => x.ToString("R")));
            var w2string = string.Join(",", ipNumerizerByIp2Vec.W2.ToColumnMajorArray().Select(x => x.ToString("R")));
            var words = new string[ipNumerizerByIp2Vec.Index2Word.Count];
            for (int i = 0; i < ipNumerizerByIp2Vec.Index2Word.Count; i++)
            {
                words[i] = ipNumerizerByIp2Vec.Index2Word[i];
            }
            var wordsarray = string.Join(",", words);
            var frequencies = string.Join(",", ipNumerizerByIp2Vec.Frequency);

            return new ModelSaver
            {
                W1String = w1string,
                W2String = w2string,
                Indexer = wordsarray,
                X = ipNumerizerByIp2Vec.W1.RowCount,
                Y = ipNumerizerByIp2Vec.W1.ColumnCount,
                Frequencies = frequencies,
            };
        }

        private static void SuDeserializer(ModelSaver data, Ip2VecModel ipNumerizerByIp2Vec)
        {
            var w1array = Array.ConvertAll(data.W1String.Split(','), double.Parse);
            var w2array = Array.ConvertAll(data.W2String.Split(','), double.Parse);
            ipNumerizerByIp2Vec.W1 = Matrix<double>.Build.Dense(data.X, data.Y, w1array);
            ipNumerizerByIp2Vec.W2 = Matrix<double>.Build.Dense(data.X, data.Y, w2array);

            var words = data.Indexer.Split(',');
            for (int i = 0; i < words.Count(); i++)
            {
                ipNumerizerByIp2Vec.Index2Word[i] = words[i];
                ipNumerizerByIp2Vec.Word2Index[words[i]] = i;
            }

            ipNumerizerByIp2Vec.Frequency = data.Frequencies.Split(',').Select(x => int.Parse(x)).ToList();
        }

        public string GetModelName(string filename)
        {
            return Definitions.GetIp2VecModelFilename(filename);
        }

        public string GetKDisCurveName(string filename)
        {
            return Definitions.GetIp2VecKDisCurveInfoFilename(filename);
        }

        public string GetInflectionPointName(string filename)
        {
            return Definitions.GetIp2VecInflectionPointFilename(filename);
        }

        public string GetClusteredFilename(string filename)
        {
            return Definitions.GetIp2VecClusteredCsvFilename(filename);
        }
    }

    public class ModelSaver
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string W1String { get; set; }

        public string W2String { get; set; }

        public string Indexer { get; set; }

        public string Frequencies { get; set; }
    }
}
