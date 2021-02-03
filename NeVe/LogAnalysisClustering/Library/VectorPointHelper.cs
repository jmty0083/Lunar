using LogAnalysisLibrary.Algorithms.Mathematics;
using LogAnalysisLibrary.DataType.VectorPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.DataType.Helper
{
    public static class VectorPointHelper
    {
        public static double GetDistance(IVectorPoint a, IVectorPoint b, Distance.GetDistance measure = null)
        {
            if (a is DoubleVectorPoint && b is DoubleVectorPoint)
            {
                return measure(((DoubleVectorPoint)a).Vector, ((DoubleVectorPoint)b).Vector);
            }
            else if (a is StringVectorPoint && b is StringVectorPoint)
            {
                return Distance.Levenshtein(((StringVectorPoint)a).Vector, ((StringVectorPoint)b).Vector);
            }
            else
            {
                throw new ArrayTypeMismatchException(string.Format("Cannot calculate distance between type {0} and type {1}", a.GetType(), b.GetType()));
            }
        }

        public static double DistanceFrom(this IVectorPoint a, IVectorPoint b, Distance.GetDistance measrue = null)
        {
            return GetDistance(a, b, measrue);
        }
    }
}
