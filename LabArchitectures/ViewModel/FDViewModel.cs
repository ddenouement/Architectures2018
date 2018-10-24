using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabArchitectures.ViewModel
{
    public class FDViewModel : INotifyPropertyChanged
    {
        private ICommand _openFileCommand;
         
        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ?? (_openFileCommand = new RelayCommand<object>(OpenFileExec));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Properties
        public string Name { get; set; }
        public Stream Stream
        {
            get;
            set;
        }

        public string Extension
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            set;
        } 
        #endregion

        private void OpenFileExec(object o)
        { 
            this.Stream = OpenFile(this.Extension, this.Filter);
        }
         
        public Stream OpenFile(string defaultExtension, string filter)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = defaultExtension;
            fd.Filter = filter;
             
            bool? result = fd.ShowDialog();
            if (result.Value  )
            {
               this.Name = fd.FileName;
                return fd.OpenFile();
            }
            else return null; 
        }
         
    }
}
