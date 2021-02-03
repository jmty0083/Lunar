using System.Text;

namespace LogAnalysisLibrary
{
    public static class Logback
    {
        public static string Logs { get => Show(); }

        public static bool UseLog { get; set; } = false;

        private static StringBuilder Sbuffer { get; set; } = new StringBuilder();

        //private static readonly string filePath = @"E:/data/ip2vec.log";


        public static void Log(string log)
        {
            if (UseLog)
            {
                Sbuffer.AppendLine(log);
            }
            //Logs = Show();
        }

        public static void Log(string log, params object[] obj)
        {
            if (UseLog)
            {
                Sbuffer.AppendFormat(log, obj).AppendLine();
            }
            //Logs = Show();
        }

        public static string Show()
        {
            return Sbuffer.ToString();
        }
    }
}
