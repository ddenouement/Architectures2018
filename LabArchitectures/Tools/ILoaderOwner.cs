
using System.ComponentModel;
using System.Windows;

namespace LabArchitectures.Tools
{
    interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsEnabled { get; set; }
    }
}

