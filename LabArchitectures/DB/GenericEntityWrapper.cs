using LabArchitectures.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures.DB
{
   public static class GenericEntityWrapper
    {

        public static void AddEntity<T>(T entity) where T : class
        {
            using (var dbContext = new DBContext())
            {
                dbContext.Set<T>().Add(entity);
                dbContext.SaveChanges();
                dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public static List<Query> GetTextRequests(Guid userId)
        {
            using (var dbContext = new DBContext())
            {
                return dbContext.Queries.AsNoTracking().Where(tr => tr.UserId == userId).ToList();
            }
        }

        public static bool IsExistingUsername(string username)
        {
            using (var dbContext = new DBContext())
            {
                return dbContext.Users.All(user => user.Login != username);
            }
        }

        public static User GetUserByName(string userName)
        {
            using (var dbContext = new DBContext())
            {
                return dbContext.Users.SingleOrDefault(user => user.Login == userName);
            }
        }

        public static void EditEntity<T>(T entity) where T : class
        {
            using (var dbContext = new DBContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
