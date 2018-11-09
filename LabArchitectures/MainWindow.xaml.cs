using LabArchitectures.Tools;
using LabArchitectures.ViewModel;
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
        public const String DataBaseSerializationFilePath = @"D:\word_count_db.txt";
        public const String LogfilePath = @"D:\word_count_logs.txt";

        public MainWindow()
        {
            InitializeComponent();
            var navigationModel = new NavModel(this);
            NavManager.Instance.Initialize(navigationModel);
            Logger.Log("NavManager Initialized");

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            Logger.Log("DataContext initialized");

            Model.ApplicationStaticDB.Deserialize(DataBaseSerializationFilePath);

            mainWindowViewModel.StartApplication();
            Logger.Log("Application started");
        }
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        
    }
        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Model.ApplicationStaticDB.Serialize(DataBaseSerializationFilePath);
            Logger.Log("Application shut down");
            System.Windows.Application.Current.Shutdown();
        }
 
         
    }
}
