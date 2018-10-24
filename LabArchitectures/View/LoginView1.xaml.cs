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

namespace LabArchitectures.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView1.xaml
    /// </summary>
    public partial class LoginView1 : UserControl
    {
        public LoginView1()
        {
            
           /* ViewModel.Auth.SignInViewModel context = new ViewModel.Auth.SignInViewModel();
            this.DataContext = context;*/
            InitializeComponent();
        }
    }
}
