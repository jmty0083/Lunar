using LogAnalysisLibrary.DataType;
using LogAnalysisLibrary.DataType.Helper;
using LogAnalysisLibrary.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering
{
    public class DataController
    {
        public string CurrentDataFilename { get; private set; } = string.Empty;

        //public string CurrentDataName => this.GetCurrentDataName();

        public List<string> Files { get; private set; } = new List<string>();

        public DataController()
        {
        }

        public void AddFile(string filename)
        {
            this.Files.Add(filename);
        }

        public void AddFiles(string path, string suffix = "*.csv")
        {
            foreach (var item in Directory.GetFiles(path, suffix))
            {
                this.AddFile(item);
            }
        }

        public IEnumerable<T> UseData<T>(string filename)
            where T : KeyFeature
        {
            using (var streamReader = new StreamReader(filename, Encoding.Default))
            {
                Console.WriteLine("Opening " + filename);
                this.CurrentDataFilename = filename;
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (typeof(T) == typeof(KeyFeature))
                    {
                        yield return line.Split(Symbols.CSVSeparator).CreateKeyFeature() as T;
                    }
                    else if (typeof(T) == typeof(KeyFeatureClustered))
                    {
                        yield return line.Split(Symbols.CSVSeparator).CreateKeyFeatureClustered() as T;
                    }
                    else if (typeof(T) == typeof(KeyFeatureClusteredCategorized))
                    {
                        yield return line.Split(Symbols.CSVSeparator).CreateKeyFeatureClusteredCategorized() as T;
                    }
                    else if (typeof(T) == typeof(KeyFeatureOfRandomEvent))
                    {
                        yield return line.Split(Symbols.CSVSeparator).CreateKeyFeatureOfRandomEvent() as T;
                    }
                    else
                    {
                        throw new TypeLoadException("Incompatible class " + typeof(T).FullName);
                    };
                }
            }
        }

        public static string GetDataNameFromFilename(string filename)
        {
            string name = filename.Substring(filename.LastIndexOf("\\") + 1);
            var adds = name.IndexOf("_");
            return adds > 0 ? name.Substring(0, adds) : name;
        }

        private string GetCurrentDataName()
        {
            return GetDataNameFromFilename(this.CurrentDataFilename);
        }
    }
}
