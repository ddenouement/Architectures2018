using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures
{
    public static class SessionContext
    {
  public    static  Model.User CurrentUser { get; set; }
        public static void LogOut()
        {
            CurrentUser = null;
        }
    }
}
