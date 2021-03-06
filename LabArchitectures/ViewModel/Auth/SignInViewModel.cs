﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using LabArchitectures.Tools;
using LabArchitectures.Managers;
using LabArchitectures.DB;
using LabArchitectures.Model;
using System.Windows;

namespace LabArchitectures.ViewModel.Auth
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        private string _password;
        private string _login;

        private ICommand _signInCommand;
        private ICommand _signUpCommand;


        #region Commands
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute, SignInCanExecute));
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }
        public SignInViewModel()
        {
        }
        private void SignUpExecute(object o)
        {
            NavManager.Instance.Navigate(ModesEnum.SingUp);
        }
        private async void SignInExecute(object obj)
        {

            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                User currentUser;
                try
                {
                    // currentUser = DB.ApplicationStaticDB.GetUserByLogin(_login);
                    currentUser = DB.GenericEntityWrapper.GetUserByName(_login);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Login Error :" + ex);
                    Logger.Log("Error getting user"+ex);
                    return false;
                }
                if (currentUser == null)
                {
                    System.Windows.MessageBox.Show(_login + " - no such users!Error!");
                    return false;
                }
                try
                {
                    if (!currentUser.CheckPassword(_password))
                    {
                        System.Windows.MessageBox.Show("Error!Wrong password!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                     System.Windows.MessageBox.Show("Error!" + ex);
                    Logger.Log("Error for user, "+ex);
                    return false;
                }
                try
                {
                    SessionContext.CurrentUser = currentUser;
                    currentUser.LastLoginDate = DateTime.Now;
                    GenericEntityWrapper.EditEntity(currentUser);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Login Error :" + e);
                    Logger.Log($"Error updating user {SessionContext.CurrentUser}"+ e);
                    return false;
                }
                //SessionContext.CurrentUser = currentUser;
                Logger.Log("User " + currentUser.Id + " signed in");
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {

               // MessageBox.Show("App started"+ ModesEnum.Main);
                NavManager.Instance.Navigate(ModesEnum.Main);
            }
        }

        private bool SignInCanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_login) && !String.IsNullOrWhiteSpace(_password);
        }


    }
}

