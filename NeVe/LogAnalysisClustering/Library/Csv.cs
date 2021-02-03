using LogAnalysisLibrary.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.DataType
{
    public class Csv
    {
        public List<string[]> Data { get; set; } = new List<string[]>();

        public void AppendLine(IEnumerable<object> line)
        {
            this.Data.Add(line.Select(x => x.ToString()).ToArray());
        }

        public void AppendLines(IEnumerable<IEnumerable<object>> line)
        {
            foreach (var item in line)
            {
                this.AppendLine(item);
            }
        }

        public void SaveCsvFile(string filename)
        {

            using (StreamWriter file = new StreamWriter(filename, false, Encoding.Default))
            {
                foreach (var line in this.Data)
                {
                    file.WriteLine(string.Join(Symbols.CSVSeparator.ToString(), line));
                }
            }
        }
    }
}
