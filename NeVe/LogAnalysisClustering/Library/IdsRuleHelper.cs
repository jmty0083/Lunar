using LogAnalysisLibrary.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.DataType.Helper
{
    public static class IdsRuleHelper
    {
        public static bool UpdateCategory(string one, string two, out bool conflict)
        {
            conflict = false;
            if (one == two)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(one))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(two))
            {
                return false;
            }
            else if (one == "bad-unknown")
            {
                return true;
            }
            else if (two == "bad-unknown")
            {
                return false;
            }
            else
            {
                conflict = true;
                return false;
            }
        }

        public static string CsvString(this IdsTag tag)
        {
            var result = new List<string>();
            foreach (Enum item in Enum.GetValues(typeof(IdsTag)))
            {
                if (tag.HasFlag(item))
                {
                    result.Add(item.ToString());
                }
            }
            return string.Join(Symbols.CSVSeparator.ToString(), result.ToArray());
        }

        ///// <summary>
        ///// Compare two timestamp to check if needs update. Format using "yyyy-MM-dd"
        ///// </summary>
        ///// <param name="one"></param>
        ///// <param name="two"></param>
        ///// <param name="conflict"></param>
        ///// <returns></returns>
        //public static bool UpdateTime(string one, string two)
        //{
        //    if (one == two)
        //    {
        //        return false;
        //    }
        //    else if (string.IsNullOrEmpty(one))
        //    {
        //        return true;
        //    }
        //    else if (string.IsNullOrEmpty(two))
        //    {
        //        return false;
        //    }

        //    var a = DateTime.ParseExact(one, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
        //    var b = DateTime.ParseExact(two, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);

        //    if (a < b)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
    }
}
