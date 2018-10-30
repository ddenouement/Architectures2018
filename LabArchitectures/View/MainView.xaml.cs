using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LabArchitectures.ViewModel;

namespace LabArchitectures.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    { 
        private MainViewModel _mainvm;
        public MainView()
        {
            Init();
            InitializeComponent(); 
                    }
        private void Init()
        {
            Visibility = Visibility.Visible;
            _mainvm = new MainViewModel();
         //   _mainvm.QueryChanged += OnQueryChanged;
            DataContext = _mainvm;
        
        }
         

    }
}
