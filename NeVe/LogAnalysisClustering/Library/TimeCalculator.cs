using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms
{
    public static class TimeCalculator
    {
        public static int MaxCountInTimeSpan(IEnumerable<DateTime> tl, TimeSpan interval)
        {
            var t = tl.OrderBy(x => x).ToList();
            int a = 0, b = 1;
            var ret = 0;
            while(b > a)
            {
                b = LastIndexBeforeTime(t, t[a] + interval, a);
                ret = Math.Max(ret, b - a + 1);
                do
                {
                    a++;
                }
                while (a < t.Count && t[a] == t[a - 1]);
            }
            return ret;
        }     

        private static int LastIndexBeforeTime(List<DateTime> tl, DateTime time, int from)
        {
            for (; from < tl.Count; from++)
            {
                if (tl[from] > time)
                {
                    break;
                }
            }
            return from - 1;
        }

        public static DateTime Timeround(this DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        public static DateTime TimeDownRound(this DateTime dt, TimeSpan d)
        {
            return new DateTime(dt.Ticks - dt.Ticks % d.Ticks);
        }

        public static List<Tuple<int,int>> GetIndexRangeByTimeInterval(List<long> timeticks, long intervalticks)
        {
            var start = 0;
            var end = 0;
            var center = 0;

            var result = new List<Tuple<int, int>>();

            do
            {
                while (end < timeticks.Count && timeticks[end] - timeticks[center] < intervalticks)
                {
                    end++;
                }

                while (start < center && timeticks[center] - timeticks[start] > intervalticks)
                {
                    start++;
                }

                result.Add(new Tuple<int, int>(start, end));
            } while (++center < timeticks.Count);

            return result;
        }
    }
}
