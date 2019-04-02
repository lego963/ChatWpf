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
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        public async Task RegisterAsync(object parameter)
        {
            await RunCommandAsync(() => RegisterIsRunning, async () =>
            {
                await Task.Delay(5000);
            });
        }

        public async Task LoginAsync()
        {
            IoC.Base.IoC.Application.GoToPage(ApplicationPage.Login);

            await Task.Delay(1);
        }
    }
}
