using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

using System.Runtime.Serialization;

namespace LabArchitectures.Model
{
    [Serializable()]
    //[DataContract(IsReference = true)]
    public class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string login;
        private string password;
        private DateTime lastLoginDate;
        private List<Query> queries;
        public Guid Id { get; set; }

        #region Properties

        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public DateTime LastLoginDate
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
          public  UserEntityConfiguration()
            {
                ToTable("Users");
                HasKey(s => s.Id);

              //  Property(u => u.Id).HasColumnName("Id").IsRequired();
                Property(p => p.LastName).HasColumnName("LastName").IsRequired();
                Property(p => p.FirstName).HasColumnName("FirstName").IsRequired();
                Property(p => p.Email).HasColumnName("Email").IsRequired();
                Property(p => p.Login).HasColumnName("Login").IsRequired();
                Property(p => p.Password).HasColumnName("Password").IsRequired();
                Property(p => p.LastLoginDate).HasColumnName("LastLoginDate").IsRequired();

                HasMany(u => u.Queries).WithRequired(q => q.User).HasForeignKey(q => q.UserId).WillCascadeOnDelete(true);
            }
        }

        #endregion
        public User()
        {

        }
        public User(string f, string l, string email, string login, string password)
        {
           FirstName = f;
            LastName = l;
            Email = email;
            Login = login;
            LastLoginDate = DateTime.Now;

            Id = Guid.NewGuid();
            Password = EncryptPassword(password);
            Queries = new List<Query>();
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
        public bool CheckPassword(User userCandidate)
        {
            try
            {
                return password == userCandidate.Password;
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
