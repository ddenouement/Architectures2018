using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LabArchitectures.Model;
using Microsoft.Win32;

namespace LabArchitectures.ViewModel
{
    class MainViewModel 
    { 
         private ObservableCollection<Query> _queries;
        private User _currentUser;
        private Query _currentQuery;
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
        public MainViewModel( )
        {

            _queries = new ObservableCollection<Query>();
            UpdCurrUser();
           
        }
         

        public void UpdCurrUser( )
        {
            this._currentUser = SessionContext.CurrentUser;
        }
      // public void createNewUser()
        public void ViewHistoryExecute(object o)
        { 
            String t = "Your query history:\n";

            foreach (Query q in _currentUser.Queries)
            {
                t += "\t";
                t += q;
                t += "\n";
            }
            MessageBox.Show(t);

        }
        public void ReadFileExecute(object obj)
        {

            /*
            string text = TextFromFD();*/
            try
            {
                OpenFile();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error..." + e);
                return;
            }


            string results = Query.GetRes(FileText);
            _currentQuery = new Query(DateTime.Now, FileName, _currentUser);
            MessageBox.Show(results);
            if (_currentQuery != null) {
              //  MessageBox.Show(_currentQuery + "adding");
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
        /*Not MVVM
         
        public String TextFromFD()
        {
            string text = "";
            OpenFileDialog opDialog = new OpenFileDialog();
            opDialog.ShowDialog(); // Show the dialog.

            try
            {
                string file = opDialog.FileName;//TODO error if close FileDialog
                if (!file.Equals(""))
                {
                    text = File.ReadAllText(file);
                    _currentQuery = new Query(DateTime.Now, file,_currentUser);
                }
            }
            catch (IOException)
            {
            }

            return text;
        }*/
    }
}
