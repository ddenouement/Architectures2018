﻿using LabArchitectures.DB;
using LabArchitectures.Managers;
using LabArchitectures.Model;
using LabArchitectures.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            List<Query> UserQueries = GenericEntityWrapper.GetTextRequests(SessionContext.CurrentUser.Id);

            foreach (var wallet in UserQueries)
            {
                _queries.Add(wallet);
            }
            if (_queries.Count > 0)
            {
                _selectedQuery = Queries[0];
            }
            
            UpdCurrUser();
          //  FillQueries();

            PropertyChanged += OnPropertyChanged;
        } 
            private  async void FillQueries()
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                List<Query> UserQueries = GenericEntityWrapper.GetTextRequests(SessionContext.CurrentUser.Id);

                foreach (var wallet in UserQueries)
                {
                    _queries.Add(wallet);
                }
                if (_queries.Count > 0)
                {
                    _selectedQuery = Queries[0];
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();

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
            Logger.Log("User " + _currentUser.Id + " viewed query history");
        }
        public async void ReadFileExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    OpenFile();
                }
                catch (Exception)
                {
                    return false;
                }

                _currentQuery = new Query(DateTime.Now, FileName, FileText, SessionContext.CurrentUser.Id);
                Logger.Log("User " + SessionContext.CurrentUser.Id + " queried file " + FileName + "\n" + _currentQuery.WordCnt + " words, " + _currentQuery.LineCnt + " lines");
                MessageBox.Show(_currentQuery + "");
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                 _queries.Add(_currentQuery);//local
                // _currentUser.AddQ(_currentQuery);//save for user

                
                try
                {
                    GenericEntityWrapper.AddEntity(_currentQuery);
                }
                catch (Exception e)
                {
                    Logger.Log("Error adding text query"+ e);
                }
            }
        }
        public void LogOutExecute(object o)
        {

            Logger.Log(_currentUser.Id +" logged out");
            SessionContext.LogOut();
            UpdCurrUser();
            //SerializeManager.RemoveFile(StationManager.UserFilePath);
            NavManager.Instance.Navigate(ModesEnum.SignIn);

        }
        private void OpenFile()
        {
            FDViewModel fdvm = new FDViewModel();
            fdvm.Extension = "*.txt";
            fdvm.Filter = "Text documents (.txt)|*.txt";
            fdvm.OpenFileCommand.Execute(null);

            if (fdvm.Stream == null)
            {
                throw new IOException("Error in reading this file");
            }

            using (StreamReader sr = new StreamReader(fdvm.Stream, Encoding.ASCII))
            {
                this._fileName = fdvm.Name;
                this._fileText = sr.ReadToEnd();
            }
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedQuery")
            {
                OnQueryChanged(_selectedQuery);
            }
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
