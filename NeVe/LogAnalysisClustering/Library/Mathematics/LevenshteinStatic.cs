using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms.Mathematics
{
    public partial class Levenshtein
    {
        public static double LevenshteinSimilarity(string origin, string target)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(target))
            {
                return 0d;
            }

            return 1.0d - GetLevenshteinDistance(origin, target) / (double)Math.Max(origin.Length, target.Length);
        }

        public static double StandardizedLevenshteinDistance(string origin, string target)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(target))
            {
                return 1d;
            }

            return GetLevenshteinDistance(origin, target) / (double)Math.Max(origin.Length, target.Length);
        }

        public static int GetLevenshteinDistance(string value1, string value2)
        {
            if (value2.Length == 0)
            {
                return value1.Length;
            }

            int[] costs = new int[value2.Length];

            // Add indexing for insertion to first row
            for (int i = 0; i < costs.Length;)
            {
                costs[i] = ++i;
            }

            for (int i = 0; i < value1.Length; i++)
            {
                // cost of the first index
                int cost = i;
                int addationCost = i;

                // cache value for inner loop to avoid index lookup and bonds checking, profiled this is quicker
                char value1Char = value1[i];

                for (int j = 0; j < value2.Length; j++)
                {
                    int insertionCost = cost;

                    cost = addationCost;

                    // assigning this here reduces the array reads we do, improvment of the old version
                    addationCost = costs[j];

                    if (value1Char != value2[j])
                    {
                        if (insertionCost < cost)
                        {
                            cost = insertionCost;
                        }

                        if (addationCost < cost)
                        {
                            cost = addationCost;
                        }

                        ++cost;
                    }

                    costs[j] = cost;
                }
            }

            return costs[costs.Length - 1];
        }
    }
}
