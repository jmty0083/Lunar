using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms
{
    public enum HeapType
    {
        MinHeap,
        MaxHeap
    }

    public class BinaryHeap<T> : IHeap<T> 
        where T : IComparable<T>
    {
        private readonly List<T> items;

        public HeapType HType { get; private set; }

        public T Root
        {
            get { return items[0]; }
        }

        public BinaryHeap(HeapType type)
        {
            items = new List<T>();
            this.HType = type;
        }

        public void Push(T item)
        {
            items.Add(item);

            int i = items.Count - 1;

            bool flag = HType == HeapType.MinHeap;

            while (i > 0)
            {
                if ((items[i].CompareTo(items[(i - 1) / 2]) > 0) ^ flag)
                {
                    T temp = items[i];
                    items[i] = items[(i - 1) / 2];
                    items[(i - 1) / 2] = temp;
                    i = (i - 1) / 2;
                }
                else
                    break;
            }
        }

        private void DeleteRoot()
        {
            int i = items.Count - 1;

            items[0] = items[i];
            items.RemoveAt(i);

            i = 0;

            bool flag = HType == HeapType.MinHeap;

            while (true)
            {
                int leftInd = 2 * i + 1;
                int rightInd = 2 * i + 2;
                int largest = i;

                if (leftInd < items.Count)
                {
                    if ((items[leftInd].CompareTo(items[largest]) > 0) ^ flag)
                        largest = leftInd;
                }

                if (rightInd < items.Count)
                {
                    if ((items[rightInd].CompareTo(items[largest]) > 0) ^ flag)
                        largest = rightInd;
                }

                if (largest != i)
                {
                    T temp = items[largest];
                    items[largest] = items[i];
                    items[i] = temp;
                    i = largest;
                }
                else
                    break;
            }
        }

        public T PopRoot()
        {
            T result = items[0];

            DeleteRoot();

            return result;
        }
    }


    public class EasyHeapSmallFixedSize<T> : IHeap<T>
        where T : IComparable<T>
    {
        private List<T> items;

        public HeapType HType { get; private set; }

        private Func<T, object> selector { get; set; }

        private readonly uint heapSize;

        private static readonly Func<T, object> default_selector = x => x;

        public EasyHeapSmallFixedSize(HeapType hType, uint size)
            :this(hType, size, default_selector)
        {
        }

        public EasyHeapSmallFixedSize(HeapType hType, uint size, Func<T, object> selector)
        {
            HType = hType;
            items = new List<T>();
            heapSize = size;
            this.selector = selector;
        }

        public T Root
        {
            get { return items[0]; }
        }

        public T PopRoot()
        {
            var t = items[0];
            items.RemoveAt(0);
            return t;
        }

        public IEnumerable<T> GetList()
        {
            while(items.Count > 0)
            {
                yield return PopRoot();
            }
        }

        public void Push(T item)
        {
            bool flag = HType == HeapType.MinHeap;
            if (items.Count < heapSize)
            {
                items.Add(item);
                switch (HType)
                {
                    case HeapType.MinHeap:
                        items = items.OrderBy(selector).ToList();
                        break;
                    case HeapType.MaxHeap:
                        items = items.OrderByDescending(selector).ToList();
                        break;
                }
            }
            else if((item.CompareTo(items.Last()) < 0) ^ flag)
            {
                return;
            }
            else
            {
                var t = items.Count;
                for (int i = 0; i < heapSize; i++)
                {
                    if ((item.CompareTo(items[i]) > 0) ^ flag)
                    {
                        items.Insert(i, item);
                        items.RemoveAt(t);
                        return;
                    }
                }
            }
        }
    }
}
