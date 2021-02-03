using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms.Mathematics
{
    //public enum Distances
    //{
    //    Euclidean = 2,
    //    Manhattan = 1
    //}

    public static class Distance
    {
        public delegate double GetDistance(double[] a, double[] b);

        //public static GetDistance Use(Distances name)
        //{
        //    switch (name)
        //    {
        //        case Distances.Euclidean:
        //            return Euclidean;
        //        case Distances.Manhattan:
        //            return Manhattan;
        //        default:
        //            throw new ArgumentOutOfRangeException("Distances measurment not exists");
        //    }
        //}

        public static double Euclidean(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                throw new IndexOutOfRangeException(string.Format("{0} is not compatible with {1} in distance measurement", a.Length, b.Length));
            }
            double ret = 0;
            for (int i = 0; i < a.Length; i++)
            {
                ret += (a[i] - b[i]) * (a[i] - b[i]);
            }
            return Math.Sqrt(ret);
        }

        public static double Manhattan(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                throw new IndexOutOfRangeException(string.Format("{0} is not compatible with {1} in distance measurement", a.Length, b.Length));
            }

            double ret = 0;
            //return a.Zip(b, (x, y) => x - y).Sum(x => x > 0 ? x : -x);
            for (int i = 0; i < a.Length; i++)
            {
                //ret += a[i] > b[i] ? a[i] - b[i] : b[i] - a[i];
                ret += Math.Abs(a[i] - b[i]);
            }
            return ret;
        }

        public static double Levenshtein(string [] a, string[] b)
        {
            if (a.Length != b.Length)
            {
                throw new IndexOutOfRangeException(string.Format("{0} is not compatible with {1} in distance measurement", a.Length, b.Length));
            }

            var result = 0d;
            for (int i = 0; i < a.Length; i++)
            {
                result += Mathematics.Levenshtein.StandardizedLevenshteinDistance(a[i], b[i]);
            }

            //Console.WriteLine(result);
            return result;
        }
    }
}
