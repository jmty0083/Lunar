using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Data.Constants
{
    public static class TimeInSeconds
    {
        public const int Minute = 60;

        public const int Hour = 60 * Minute;

        public const int Day = 24 * Hour;

        public const int Week = 7 * Day;

        public static long SecondsTick(this DateTime datetime)
        {
            return datetime.Ticks / 10000000;
        }
    }
}
