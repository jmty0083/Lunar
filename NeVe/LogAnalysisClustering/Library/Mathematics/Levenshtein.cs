﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.Algorithms.Mathematics
{
    public partial class Levenshtein
    {
        private readonly string storedValue;
        private readonly int[] costs;

        /// <summary>
        /// Creates a new instance with a value to test other values against
        /// </summary>
        /// <param Name="value">Value to compare other values to.</param>
        public Levenshtein(string value)
        {
            this.storedValue = value;
            // Create matrix row
            this.costs = new int[this.storedValue.Length];
        }

        /// <summary>
        /// gets the length of the stored value that is tested against
        /// </summary>
        public int StoredLength
        {
            get
            {
                return this.storedValue.Length;
            }
        }

        public double Similarity(string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return 0d;
            }

            return 1.0d - DistanceFrom(target) / (double)Math.Max(storedValue.Length, target.Length);
        }

        public double StandardizedDistance(string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return 1d;
            }

            return DistanceFrom(target) / (double)Math.Max(storedValue.Length, target.Length);
        }

        /// <summary>
        /// Compares a value to the stored value. 
        /// Not thread safe.
        /// </summary>
        /// <returns>Difference. 0 complete match.</returns>
        public int DistanceFrom(string value)
        {
            if (costs.Length == 0)
            {
                return value.Length;
            }

            // Add indexing for insertion to first row
            for (int i = 0; i < this.costs.Length;)
            {
                this.costs[i] = ++i;
            }

            for (int i = 0; i < value.Length; i++)
            {
                // cost of the first index
                int cost = i;
                int addationCost = i;

                // cache value for inner loop to avoid index lookup and bonds checking, profiled this is quicker
                char value1Char = value[i];

                for (int j = 0; j < this.storedValue.Length; j++)
                {
                    int insertionCost = cost;

                    cost = addationCost;

                    // assigning this here reduces the array reads we do, improvment of the old version
                    addationCost = this.costs[j];

                    if (value1Char != this.storedValue[j])
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

                    this.costs[j] = cost;
                }
            }

            return this.costs[this.costs.Length - 1];
        }
    }
}
