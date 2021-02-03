using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms
{
    public interface IHeap<T>
    {
        T Root { get; }

        void Push(T item);

        T PopRoot();
    }
}
