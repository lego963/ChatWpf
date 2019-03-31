using System.Security;
using System.Windows;
using ChatWpf.Core;
using ChatWpf.Core.ViewModel;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
    {
        public SecureString SecurePassword => PasswordText.SecurePassword;

        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
