using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures.Tools
{
    /// <summary>
    /// Utility for logging
    /// </summary>
    static class Logger
    {

        private static String logfilePath = MainWindow.LogfilePath;

        /// <summary>
        /// Set new logfile
        /// </summary>
        /// <param name="newLogfilePath"> path to new logfile</param>
        public static void SetLogfile(String newLogfilePath)
        {
            if (!isFilepathValid(newLogfilePath))
            {
                throw new ArgumentException("Invalid Filepath");
            }

            logfilePath = newLogfilePath;
        }

        /// <summary>
        /// Log str to currently selected logfile
        /// </summary>
        /// <param name="str">String to log</param>
        public static void Log(String str)
        {
            File.AppendAllLines(logfilePath, new[] { DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"), str, "" });
        }

        public static void LogErr(String str)
        {
            Log(str.ToUpper() + "\n");
        }

        private static bool isFilepathValid(String filepath)
        {
            try
            {
                // These two methods throw exceptions if The path parameter contains invalid characters, is empty, or contains only white spaces.
                Path.GetDirectoryName(filepath);
                Path.GetFileName(filepath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
