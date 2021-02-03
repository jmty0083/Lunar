using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Systems
{
    public class Coordinate<T, K>
    {
        public T X { get; set; }

        public K Y { get; set; }

        public static Coordinate<T,K> NewInstance(T x, K y)
        {
            return new Coordinate<T, K>()
            {
                X = x,
                Y = y,
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate<T, K> coordiate &&
                   EqualityComparer<T>.Default.Equals(X, coordiate.X) &&
                   EqualityComparer<K>.Default.Equals(Y, coordiate.Y);
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(X);
            hashCode = hashCode * -1521134295 + EqualityComparer<K>.Default.GetHashCode(Y);
            return hashCode;
        }
    }
}
