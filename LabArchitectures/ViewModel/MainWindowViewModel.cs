using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LabArchitectures.ViewModel
{
    class MainWindowViewModel:Tools.ILoaderOwner

    {
        #region LoaderIntefaceImpl
        private Visibility _visibility = Visibility.Hidden;
        private bool _isEnabled = true;
        public Visibility LoaderVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

          #endregion
        public MainWindowViewModel()
        {
            Managers.LoaderManager.Instance.Initialize(this);

        }

        internal void StartApplication()
        {
            NavManager.Instance.Navigate(SessionContext.CurrentUser != null ? ModesEnum.Main : ModesEnum.SignIn);
        }

        public event PropertyChangedEventHandler PropertyChanged;
         
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
