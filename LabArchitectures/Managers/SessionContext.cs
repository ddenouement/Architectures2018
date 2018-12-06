using LabArchitectures.DB;
using LabArchitectures.Model;
using LabArchitectures.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures
{
    public static class SessionContext
    { static SessionContext()
        {
            DeserializeLastUser();
        }
        private static void DeserializeLastUser()
        {
            User userCandidate;
            try
            {
                userCandidate = Serializer.Deserialize<User>(Path.Combine(StaticResources.LastUserFilePath));
            }
            catch (Exception ex)
            {
                userCandidate = null;
                Logger.Log("Failed to Deserialize last user"+ ex);
            }
            if (userCandidate == null)
            {
                Logger.Log("User was not deserialized");
                return;
            }
            userCandidate = CheckCachedUser(userCandidate);
            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
                CurrentUser = userCandidate;
        }
        public static User CurrentUser { get; set; }
        public static void LogOut()
        {
            Logger.Log(CurrentUser.Id + " logged out");
            CurrentUser = null;
        }
        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = GenericEntityWrapper.GetUserByName(userCandidate.Login);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }
    }
}
