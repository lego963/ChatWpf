using System.Windows.Input;
using ChatWpf.Core.ApiModels;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.Security;
using ChatWpf.ViewModel.Base;
using ChatWpf.WebRequests;
using static ChatWpf.DI.DI;

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
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        public async System.Threading.Tasks.Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                // Call the server and attempt to login with credentials
                // TODO: Move all URLs and API routes to static class in core
                var result = await Dna.WebRequests.PostAsync<ApiResponse<LoginResultApiModel>>(
                    "http://localhost:5000/api/login",
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                    });

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Login Failed"))
                    // We are done
                    return;

                // OK successfully logged in... now get users data
                var loginResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the successful login
                await ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        public async System.Threading.Tasks.Task RegisterAsync()
        {
            // Go to register page?
            ViewModelApplication.GoToPage(ApplicationPage.Register);

            await System.Threading.Tasks.Task.Delay(1);
        }
    }
}
