using System.Security;
using ChatWpf.ViewModel.Application;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.Pages
{
    public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
    {
        public SecureString SecurePassword => PasswordText.SecurePassword;

        public RegisterPage()
        {
            InitializeComponent();
        }

        public RegisterPage(RegisterViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

    }
}
