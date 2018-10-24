using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LabArchitectures
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    //   public ISessionContext isc;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
             
           /* View.ApplicationView app = new View.ApplicationView();
            ViewModel.ApplicationViewModel context = new ViewModel.ApplicationViewModel();
            app.DataContext = context;*/
           // app.Show();
            //
        }
        void mw_Loaded(object sender, RoutedEventArgs e)
        {   // loaded event comes here
            
        }
    }
}
