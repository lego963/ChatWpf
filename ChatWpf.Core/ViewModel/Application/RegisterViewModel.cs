using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Application
{
    public class RegisterViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public bool RegisterIsRunning { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            // Create commands
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        public async System.Threading.Tasks.Task RegisterAsync(object parameter)
        {
            await RunCommandAsync(() => RegisterIsRunning, async () =>
            {
                await System.Threading.Tasks.Task.Delay(5000);
            });
        }

        public async System.Threading.Tasks.Task LoginAsync()
        {
            // Go to register page?
            IoC.Base.IoC.Application.GoToPage(ApplicationPage.Login);

            await System.Threading.Tasks.Task.Delay(1);
        }
    }
}
