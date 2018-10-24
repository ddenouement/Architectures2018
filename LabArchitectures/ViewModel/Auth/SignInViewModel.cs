using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabArchitectures.ViewModel.Auth
{
    public class SignInViewModel :  INotifyPropertyChanged
    {
        private string _password;
        private string _login;

       // private ICommand _closeCommand;
        private ICommand _signInCommand;
        private ICommand _signUpCommand;

         
        #region Commands
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute));
            }
        }
        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute));
            }
        }
        #endregion
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged( );
            }
        }
        public string Login
        {
            get { return _login; }
            set
            { 
                    _login = value;
                    OnPropertyChanged( );
                 
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }
        /*
        public event PropertyChangedEventHandler PropertyChanged;
        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
      public    SignInViewModel()
        {
        }
        private void SignUpExecute(object o)
        {
            NavManager.Instance.Navigate(ModesEnum.SingUp);
        }
        private void SignInExecute(object obj)
        {
            Model.User currentUser;
            try
            {
                currentUser = Model.ApplicationStaticDB.GetUserByLogin(_login);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Login Error :"+ex);
                return;
            }
            if (currentUser == null)
            {
                System.Windows.MessageBox.Show( _login+" - no such users!Error!");
                return;
            }
            try
            {
                if (!currentUser.CheckPassword(_password))
                {
                    System.Windows.MessageBox.Show("Error!Wrong password!");
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error!"+ ex);
                return;
            }
            //CommandBinding NewCmd = new CommandBinding();
            //NewCmd.Command.Execute("parameter");

            SessionContext.CurrentUser = currentUser;
            System.Windows.MessageBox.Show("Login Successfull");
             
              NavManager.Instance.Navigate(ModesEnum.Main);
             
        }

        private bool SignInCanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_login) && !String.IsNullOrWhiteSpace(_password);
        }

       
    }
}

