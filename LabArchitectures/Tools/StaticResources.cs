using System;
using System.IO;

namespace LabArchitectures.Tools
{

    public static class StaticResources
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "WordCount");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        public static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.wordc");

        public static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.wordc");

        public static void CheckAndCreateFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                if (!file.Exists)
                {
                    file.Create().Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
