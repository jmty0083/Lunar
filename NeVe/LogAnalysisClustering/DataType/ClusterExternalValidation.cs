using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.DataType
{
    public class ClusterExternalValidation
    {
        public long TruePositive { get; set; }

        public long TrueNegative { get; set; }

        public long FalsePositive { get; set; }

        public long FalseNegative { get; set; }

        public string Print()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("TP : {0}", this.TruePositive).AppendLine();
            sb.AppendFormat("TN : {0}", this.TrueNegative).AppendLine();
            sb.AppendFormat("FP : {0}", this.FalsePositive).AppendLine();
            sb.AppendFormat("FN : {0}", this.FalseNegative).AppendLine();
            sb.AppendFormat("Precision : {0:0.000} %", this.Precision * 100).AppendLine();
            sb.AppendFormat("Recall : {0:0.000} %", this.Recall * 100).AppendLine();
            sb.AppendFormat("F-1 : {0:0.000} %", this.F1score * 100).AppendLine();
            sb.AppendFormat("Rand Coefficient : {0:0.000} %", this.RandCoefficient * 100).AppendLine();
            sb.AppendFormat("FM Coefficient : {0:0.000} %", this.FMCoefficient * 100).AppendLine();
            sb.AppendFormat("Jaccard Coefficient : {0:0.000} %", this.JaccardCoefficient * 100).AppendLine();
            sb.AppendFormat("Purity : {0:0.000} %", this.Purity * 100).AppendLine();
            sb.AppendFormat("NMI : {0:0.000} ", this.NMI).AppendLine();

            return sb.ToString();
        }

        public double Precision => (double)this.TruePositive / (this.TruePositive + this.FalsePositive);

        public double Recall => (double)this.TruePositive / (this.TruePositive + this.FalseNegative);

        public double F1score => this.Fmeasure(1);

        public double RandCoefficient => (double)(this.TruePositive + this.TrueNegative) / (double)(this.TruePositive + this.TrueNegative + this.FalsePositive + this.FalseNegative);

        public double FMCoefficient => Math.Sqrt((double)this.TruePositive * this.TruePositive / (this.TruePositive + this.FalsePositive) / (this.TruePositive + this.FalseNegative));

        public double JaccardCoefficient => (double)this.TruePositive / (this.TruePositive + this.FalsePositive + this.FalseNegative);

        public double Fmeasure(int alpha)
        {
            return ((double)alpha + 1) / (1 / (double)this.Precision + (double)alpha / this.Recall);
        }

        public double Purity { get;set; }

        public double NMI { get; set; }
    }
}
