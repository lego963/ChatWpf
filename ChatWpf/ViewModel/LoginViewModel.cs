using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Security;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public bool LoginIsRunning { get; set; }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
        }

        public async Task Login(object parameter)
        {
            await RunCommand(() => this.LoginIsRunning, async () =>
            {
                await Task.Delay(5000);

                var email = this.Email;

                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });
        }
    }
}