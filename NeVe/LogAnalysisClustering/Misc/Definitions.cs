using LogAnalysisClustering.Vectorize;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model;
using LogAnalysisClustering.Vectorize.NeVeEmbedding.Model;
using LogAnalysisClustering.Vectorize.StringEmbedding;
using LogAnalysisLibrary.Data.Constants;
using LogAnalysisLibrary.DataType;
using LogAnalysisLibrary.DataType.VectorPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisClustering.Misc
{
    public static partial class Definitions
    {
        private static Func<double, double, double> Sum = (a, b) => a + b;

        public static VectorPointBase ToVectorPoint(this KeyFeature data, IEmbeddingModel model, int id)
        {
            if (model is Ip2VecModel || model is NeVeModel)
            {
                return new DoubleVectorPoint
                {
                    TimeTick = DateTime.Parse(data.Timestamp).SecondsTick(),
                    Vector = data.VectorByModel(model),
                    Id = id
                };
            }
            else if (model is StringEmbeddingModel)
            {
                return new StringVectorPoint
                {
                    TimeTick = DateTime.Parse(data.Timestamp).SecondsTick(),
                    Vector = ((StringEmbeddingModel)model).GetStringVector(data),
                    Id = id
                };
            }
            else
            {
                throw new InvalidCastException(string.Format("Unknown type of {0}", model.GetType()));
            }
        }

        public static VectorPointBase ToVectorPoint(this KeyFeatureClusteredCategorized data, IEmbeddingModel model)
        {
            return ToVectorPoint(data, model, data.Id);
        }

        public static VectorPointBase ToVectorPoint(this KeyFeatureClustered data, IEmbeddingModel model)
        {
            return ToVectorPoint(data, model, data.Id);
        }

        public static double[] VectorByModel(this KeyFeature x, IEmbeddingModel model)
        {
            return model.LookupVectorByWord(x.SourceIp)
                        .Zip(model.LookupVectorByWord(x.TargetIp), Sum)
                        .Zip(model.LookupVectorByWord(x.TargetPort), Sum)
                        .Zip(model.LookupVectorByWord(x.Protocol), Sum)
                        .ToArray();
        }

        public static string GetIp2VecModelFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultIp2VecModelSuffix;
        }

        public static string GetNeVeModelFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultNeVeModelSuffix;
        }

        public static string GetIp2VecKDisCurveInfoFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultIp2VecKDisCurveInfoSuffix;
        }

        public static string GetIp2VecInflectionPointFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultIp2VecInflectionPointSuffix;
        }

        public static string GetGloVeKDisCurveInfoFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultNeVeKDisCurveInfoSuffix;
        }

        public static string GetGloVeInflectionPointFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultNeVeInflectionPointSuffix;
        }

        public static string GetStringKDisCurveInfoFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultStringKDisCurveInfoSuffix;
        }

        public static string GetStringInflectionPointFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultStringInflectionPointSuffix;
        }

        public static string GetIp2VecClusteredCsvFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultIp2VecClusteredCSVSuffix;
        }

        public static string GetGloVeClusteredCsvFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultNeVeClusteredCSVSuffix;
        }

        public static string GetStringClusteredCsvFilename(string filename)
        {
            var dataname = DataController.GetDataNameFromFilename(filename);
            return Constants.DefaultDebugLoggingDirectory + dataname + Constants.DefaultStringClusteredCSVSuffix;
        }
    }
}
