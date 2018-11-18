using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using LabArchitectures.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures.Model
{
    [Serializable()]
    public class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string login;
        private string password;
        private DateTime lastLoginDate;
        private List<Query> queries;
        public int uniqueID;

        #region Properties

        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        private DateTime LastLoginDate
        {
            get
            {
                return lastLoginDate;
            }
            set
            {
                lastLoginDate = value;
            }
        }

        public int ID
        {
            get
            {
                return uniqueID;
            }
            set
            {
                uniqueID = value;
            }
        }

        public List<Query> Queries
        {
            get
            {
                return queries;
            }
            private set
            {
                queries = value;
            }
        }
        public string Login
        {
            get
            {
                return login;
            }
            private set
            {
                login = value;
            }
        }
        private string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        #endregion


        #region EntityConfiguration
        public class UserEntityConfiguration : EntityTypeConfiguration<User>
        {
            UserEntityConfiguration()
            {
                ToTable("Users");
                HasKey(s => s.uniqueID);

                Property(u => u.ID).HasColumnName("UserID").IsRequired();
                Property(p => p.LastName).HasColumnName("LastName").IsRequired();
                Property(p => p.FirstName).HasColumnName("FirstName").IsRequired();
                Property(p => p.Email).HasColumnName("Email").IsRequired();
                Property(p => p.Login).HasColumnName("Login").IsRequired();
                Property(p => p.Password).HasColumnName("Password").IsRequired();
                Property(p => p.LastLoginDate).HasColumnName("LastLoginDate").IsRequired();

                HasMany(u => u.Queries).WithRequired(q => q.User).HasForeignKey(q => q.UserID).WillCascadeOnDelete(true);
            }
        }

        #endregion

        public User(string firstName, string lastName, string email, string login, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.login = login;
            lastLoginDate = DateTime.Now;
            uniqueID = ApplicationStaticDB.GetNewID();
            this.password = EncryptPassword(password);
            queries = new List<Query>();
        }
        public void AddQ(Query q)
        {
            this.queries.Add(q);
        }
        public bool CheckPassword(string p)
        {
            try
            {
                return (password) == EncryptPassword(p);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string EncryptPassword(String p)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(p);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }


    }
}
