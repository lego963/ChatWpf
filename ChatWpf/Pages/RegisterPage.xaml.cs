using System.Security;
using ChatWpf.Core;

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
