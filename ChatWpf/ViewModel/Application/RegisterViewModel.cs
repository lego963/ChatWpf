using System.Windows.Input;
using ChatWpf.Core.ApiModels;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.Security;
using ChatWpf.ViewModel.Base;
using ChatWpf.WebRequests;
using static ChatWpf.DI.DI;

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
            // Create commands
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        public async System.Threading.Tasks.Task RegisterAsync(object parameter)
        {
            await RunCommandAsync(() => RegisterIsRunning, async () =>
            {
                // Call the server and attempt to register with the provided credentials
                // TODO: Move all URLs and API routes to static class in core
                var result = await Dna.WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                    "http://localhost:6600/api/register",
                    new RegisterCredentialsApiModel
                    {
                        Username = Username,
                        Email = Email,
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                    });

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Register Failed"))
                    // We are done
                    return;

                // OK successfully registered (and logged in)... now get users data
                var loginResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the successful login
                await ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        public async System.Threading.Tasks.Task LoginAsync()
        {
            // Go to register page?
            ViewModelApplication.GoToPage(ApplicationPage.Login);

            await System.Threading.Tasks.Task.Delay(1);
        }
    }
}
