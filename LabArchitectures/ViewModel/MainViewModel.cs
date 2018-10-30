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
using LabArchitectures.Model;
using Microsoft.Win32;

namespace LabArchitectures.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Query> _queries;
        private User _currentUser;
        private Query _currentQuery;
        private Query _selectedQuery;
        private string _fileName;
        private string _fileText;
        private ICommand _readFileCommand;
        private ICommand _viewHistoryCommand;
        private ICommand _logOutCommand;
        #region ICommands
        public ICommand ReadFileCommand
        {
            get { return _readFileCommand ?? (_readFileCommand = new RelayCommand<object>(ReadFileExecute)); }
        }
        public ICommand ViewHistoryCommand
        {
            get { return _viewHistoryCommand ?? (_viewHistoryCommand = new RelayCommand<object>(ViewHistoryExecute)); }
        }

        public ICommand LogOutCommand
        {
            get { return _logOutCommand ?? (_logOutCommand = new RelayCommand<object>(LogOutExecute)); }
        }
        #endregion
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set { _fileName = value; }
        }
        public string FileText
        {
            get
            {
                return _fileText;
            }
            set { _fileText = value; }
        }
        public ObservableCollection<Query> Queries
        {
            get { return _queries; }
        }
        public Query SelectedQuery
        {
            get { return _selectedQuery; }
            set
            {
                _selectedQuery = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {

            _queries = new ObservableCollection<Query>();
            UpdCurrUser();
            FillQueries();

            PropertyChanged += OnPropertyChanged;
        }
        private void FillQueries()
        {
            foreach (var wallet in _currentUser.Queries)
            {
                _queries.Add(wallet);
            }
            if (_queries.Count > 0)
            {
                _selectedQuery = Queries[0];
            }
        }
        public void UpdCurrUser()
        {
            this._currentUser = SessionContext.CurrentUser;
        }

        public void ViewHistoryExecute(object o)
        {
            String t = "Your query history:\n";

            foreach (Query q in _currentUser.Queries)
            {
                t += q;
                t += "\n";
            }
            MessageBox.Show(t);

        }
        public void ReadFileExecute(object obj)
        {
            try
            {
                OpenFile();
            }
            catch (Exception e)
            {

                return;
            }

            _currentQuery = new Query(DateTime.Now, FileName, FileText);
            MessageBox.Show(_currentQuery + "");
            if (_currentQuery != null)
            {
                _queries.Add(_currentQuery);//local
                _currentUser.AddQ(_currentQuery);//save for user
            }
        }
        public void LogOutExecute(object o)
        {
            SessionContext.LogOut();
            UpdCurrUser();
            NavManager.Instance.Navigate(ModesEnum.SignIn);

        }
        private void OpenFile()
        {
            FDViewModel fdvm = new FDViewModel();
            fdvm.Extension = "*.txt";
            fdvm.Filter = "Text documents (.txt)|*.txt";
            fdvm.OpenFileCommand.Execute(null);

            if (fdvm.Stream == null)
                throw new IOException("Error in reading this file");

            using (StreamReader sr = new StreamReader(fdvm.Stream, Encoding.ASCII))
            {
                this._fileName = fdvm.Name;
                this._fileText = sr.ReadToEnd();
            }
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedQuery")
                OnQueryChanged(_selectedQuery);
        }

        #region Loader
        internal event QueryChangedHandler QueryChanged;
        internal delegate void QueryChangedHandler(Query q);

        internal virtual void OnQueryChanged(Query q)
        {
            QueryChanged?.Invoke(q);
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
