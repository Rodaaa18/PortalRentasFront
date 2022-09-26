using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace AppWebRentas.Models
{
    public static class Logger
    {
        #region PrivateMembers
        private static string logPath;
        #endregion

        #region Properties
        public static string LogPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(logPath))
                    logPath = ConfigurationManager.AppSettings["LogPath"];

                return logPath;
            }
        }
        #endregion

        #region BehaviorMethods

        public static void Log(Exception ex, string title = null)
        {
            var stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod().Name;

            Log(ex.Message, callerMethod, ex.StackTrace, title);
            if (ex.InnerException != null)
                Log(ex.InnerException, "InnerException");
        }

        public static void Log(string message, string callerMethod, string stackTrace = null, string title = null)
        {
            var path = $"{LogPath}siteLog{DateTime.Now.ToString("yyyyMMdd")}.txt";
            var separator = "|";
            var logLine = string.Format("{1}{0}{2}{0}{3}{0}{4}", separator, DateTime.Now, callerMethod, title, message);
            using (var file = File.AppendText(path))
            {
                file.WriteLine(logLine);
                file.WriteLine(stackTrace);
            }
        }

        #endregion
    }
}