using System.Security;
using ChatWpf.Core.ViewModel;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
    {
        public SecureString SecurePassword => PasswordText.SecurePassword;

        public RegisterPage()
        {
            InitializeComponent();
        }
    }
}
