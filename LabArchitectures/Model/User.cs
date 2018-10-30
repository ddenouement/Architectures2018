using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures.Model
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private string _password;
        private DateTime _lastLoginDate;
        private List<Query> _queries;
        public int _uniqueID;

        #region Properties
        private DateTime LastLoginDate
        {
            get
            {
                return _lastLoginDate;
            }
            set
            {
                _lastLoginDate = value;
            }
        }

        public int ID
        {
            get
            {
                return _uniqueID;
            }
            set
            {
                _uniqueID = value;
            }
        }

        public List<Query> Queries
        {
            get
            {
                return _queries;
            }
            private set
            {
                _queries = value;
            }
        }
        public string Login
        {
            get
            {
                return _login;
            }
            private set
            {
                _login = value;
            }
        }
        private string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        #endregion

        public User(string firstName, string lastName, string email, string login, string password)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _login = login;
            _lastLoginDate = DateTime.Now;
            _uniqueID = ApplicationStaticDB.GetNewID();
            _password = EncryptPassword(password);
            _queries = new List<Query>();
        }
        public void AddQ(Query q)
        {
            this._queries.Add(q);
        }
        public bool CheckPassword(string p)
        {
            try
            {
                return (_password) == EncryptPassword(p);
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
