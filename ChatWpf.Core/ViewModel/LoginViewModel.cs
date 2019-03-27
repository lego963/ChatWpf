using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.Security;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public bool LoginIsRunning { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        public async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                await Task.Delay(5000);

                var email = Email;

                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });
        }

        public async Task RegisterAsync()
        {
            IoC.IoC.Get<ApplicationViewModel>().SideMenuVisible ^= true;
            return;

            IoC.IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.Register;

            await Task.Delay(1);
        }
    }
}