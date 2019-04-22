using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Core.ApiModels;
using ChatWpf.Core.ApiModels.LoginRegister;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.Routes;
using ChatWpf.Core.Security;
using ChatWpf.ViewModel.Base;
using ChatWpf.WebRequests;

namespace ChatWpf.ViewModel.Application
{
    public class RegisterViewModel : BaseViewModel
    {
        public string Username { get; set; }

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
                var result = await Dna.WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.Register),
                    new RegisterCredentialsApiModel
                    {
                        Username = Username,
                        Email = Email,
                        Password = (parameter as IHavePassword)?.SecurePassword.Unsecure()
                    });

                if (await result.HandleErrorIfFailedAsync("Register Failed"))
                    return;

                var loginResult = result.ServerResponse.Response;

                await DI.Di.ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        public async Task LoginAsync()
        {
            DI.Di.ViewModelApplication.GoToPage(ApplicationPage.Login);

            await Task.Delay(1);
        }
    }
}
