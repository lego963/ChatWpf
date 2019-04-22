using System.Security;
using ChatWpf.ViewModel.Application;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.Pages
{
    public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
    {
        public SecureString SecurePassword => PasswordText.SecurePassword;

        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(LoginViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

    }
}
