using LabArchitectures.Managers;
using LabArchitectures.Model;
using LabArchitectures.Tools;
using LabArchitectures.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LabArchitectures.ViewModel.Auth
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        private string _password;
        private string _login;
        private string _email;
        private string _name;
        private string _lastname;

        //private ICommand _closeCommand;
        private ICommand _signInCommand;
        private ICommand _signUpCommand;

        #region fieldProperties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region ICommands
        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute, SignUpCanExecute));
            }
        }
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute));
            }
        }
        #endregion
        private bool SignUpCanExecute(object obj)
        {
            return !String.IsNullOrEmpty(_login) &&
                   !String.IsNullOrEmpty(_password) &&
                   !String.IsNullOrEmpty(_name) &&
                   !String.IsNullOrEmpty(_lastname) &&
                   !String.IsNullOrEmpty(_email);
        }
        private void SignInExecute(object obj)
        {
            NavManager.Instance.Navigate(ModesEnum.SignIn);
        }
        private async void SignUpExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Model.User currentUser;
                try
                {
                    if (ApplicationStaticDB.GetUserByLogin(_login) != null)
                    {
                        MessageBox.Show("Try up new name! This user already exists: " + _login);
                        return false;
                    }
                    if (!IsValidEmail(_email))
                    {
                        MessageBox.Show("Invalid e-mail: " + _email);
                        return false;
                    }
                    currentUser = new User(_name, _lastname, _email, _login, _password);
                    ApplicationStaticDB.AddUser(currentUser);
                    SessionContext.CurrentUser = currentUser;
                    Logger.Log("User " + currentUser.ID + " signed up");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error! " + ex);
                    return false;
                }
            });
            LoaderManager.Instance.HideLoader();
            if (result)
                NavManager.Instance.Navigate(ModesEnum.Main);

        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
