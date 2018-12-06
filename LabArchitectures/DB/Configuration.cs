using LabArchitectures.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabArchitectures.DB
{
    internal sealed class Configuration : DbMigrationsConfiguration<DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DBContext context)
        {
            if (context.Users.Any()) return;
            
            try
            { User u = new User("ss", "dd", "asas1@gmail.com", "1", "1");
                 context.Users.Add(u);
                 context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                         MessageBox.Show(validationError.PropertyName+ validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}

