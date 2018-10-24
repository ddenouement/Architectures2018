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

namespace LabArchitectures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ViewModel.IPageViewModel
    {
        public MainWindow()
        {
            InitializeComponent();
            var navigationModel = new NavModel(this);
            NavManager.Instance.Initialize(navigationModel);
            navigationModel.Navigate(ModesEnum.SignIn);
            Model.ApplicationStaticDB.AddUser(new Model.User("Julia", "Aleksandrova", "abrakadabra@gmail.com", "ddenouement", "12345"));
        }
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        
    }
        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
 
         
    }
}
