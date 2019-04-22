using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Core.ApiModels;
using ChatWpf.Core.ApiModels.LoginRegister;
using ChatWpf.Core.ApiModels.UserProfile;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.Routes;
using ChatWpf.Core.Security;
using ChatWpf.ViewModel.Base;
using ChatWpf.WebRequests;

namespace ChatWpf.ViewModel.Application
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
                var result = await Dna.WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.Login),
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = ((IHavePassword) parameter).SecurePassword.Unsecure()
                    });

                if (await result.HandleErrorIfFailedAsync("Login Failed"))
                    return;

                var loginResult = result.ServerResponse.Response;

                await DI.Di.ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        public async Task RegisterAsync()
        {
            DI.Di.ViewModelApplication.GoToPage(ApplicationPage.Register);

            await Task.Delay(1);
        }
    }
}
