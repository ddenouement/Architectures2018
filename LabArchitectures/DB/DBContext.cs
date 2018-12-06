using LabArchitectures.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures.DB
{
    public class DBContext : DbContext
    {
        public DBContext() : base("MsSqlConnectionString")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DBContext, Configuration>("MsSqlConnectionString"));


        }

        public DbSet<User> Users { get; set; }

        public DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Query.QueryEntityConfiguration());
        }
    }
}
