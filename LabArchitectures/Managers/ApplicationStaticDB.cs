using LabArchitectures.Tools;
using LabArchitectures.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LabArchitectures.Model
{
    static class ApplicationStaticDB
    {
        private static List<User> Users = new List<User>();


        public static User GetUserByLogin(string l)
        {
            return (Users.Where(i => i.Login == l).FirstOrDefault());
        }

        public static void AddUser(User user)
        {
            Users.Add(user);
        }
        public static bool GetUserByID(int a)
        {
            return Users.Where(i => i.ID == a).FirstOrDefault() != null;
        }
        public static int GetNewID()
        {
            return Guid.NewGuid().GetHashCode();
        }

        public static void Serialize(String filePath)
        {
            String directoryName = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryName);

            using (FileStream fs = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, Users);
            }
            Logger.Log("Database saved to " + filePath);

        }

        public static void Deserialize(String filePath)
        {
            if (!File.Exists(filePath))
            {
                Logger.LogErr("failed to load database, file " + filePath + " doesn't exist");
                return;
            }
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Users = (List<User>)bf.Deserialize(fs);
            }
            Logger.Log("Database loaded from " + filePath);
        }
    }
}
