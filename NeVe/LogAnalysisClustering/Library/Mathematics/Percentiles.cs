using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms.Mathematics
{
    public static class Percentiles
    {
        public static bool IsOrdered<T>(this IEnumerable<T> list, IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            if (list.Count() > 2)
            {
                var i = 1;
                while (i < list.Count() && comparer.Compare(list.ElementAt(i), list.ElementAt(i - 1)) == 0)
                {
                    i++;
                }

                if (i < list.Count())
                {
                    var s = comparer.Compare(list.ElementAt(i), list.ElementAt(i - 1));

                    for (i += 1; i < list.Count(); i++)
                    {
                        var t = comparer.Compare(list.ElementAt(i), list.ElementAt(i - 1));
                        if (t * s < 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static T Median<T>(this IEnumerable<T> list)
        {
            var s = list;
            if (!s.IsOrdered())
            {
                s = s.OrderBy(x => x);
            }

            var c = s.Count();

            if (c % 2 == 0)
            {
                dynamic a = list.ElementAt(c / 2);
                dynamic b = list.ElementAt(c / 2 - 1);
                return (a + b) / 2;
            }
            else
            {
                return list.ElementAt(c / 2);
            }
        }


        public static double[] Percentile<T>(this T[] sequence, params double[] excelPercentiles)
        {
            Array.Sort(sequence);

            int N = sequence.Length;
            List<double> result = new List<double>();
            foreach (var excelPercentile in excelPercentiles)
            {
                double n = (N - 1) * excelPercentile + 1;
                // Another method: double n = (N + 1) * excelPercentile;
                int k = (int)n;
                double d = n - k;
                dynamic a = sequence[k - 1];
                dynamic b = sequence[k];
                result.Add(a + d * (b - a));
            }
            return result.ToArray();
        }
    }

}
