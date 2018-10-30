using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LabArchitectures.Model;
using Microsoft.Win32;
using System.IO;

namespace LabArchitectures.Model
{
    class ApplicationStaticDB
    {
         private static   List<User> Users = new List<User>(); 
      

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
            return Users.Where(i => i.ID == a).FirstOrDefault()!=null;
        }
        public static int GetNewID()
        {
            return Guid.NewGuid().GetHashCode();
        }
    }
}
