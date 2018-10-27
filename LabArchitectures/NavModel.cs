using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabArchitectures
{

    internal enum ModesEnum
    {
        SignIn,
        SingUp,
        Main
    }

    internal class NavModel
    {
        private readonly ViewModel.IPageViewModel _contentWindow;
        private View.LoginView1 _signInView;
        private View.SignUpView _signUpView;
        private View.MainView _mainView;

        internal NavModel(ViewModel.IPageViewModel contentWindow)
        {
            _contentWindow = contentWindow;
        }
        
        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.SignIn:
                    _contentWindow.ContentControl.Content = _signInView ?? (_signInView = new View.LoginView1());
                    break;
                case ModesEnum.SingUp:
                    _contentWindow.ContentControl.Content = _signUpView ?? (_signUpView = new View.SignUpView());
                    break;
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = (_mainView = new View.MainView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

    }
}