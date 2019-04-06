using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.Input;

namespace ChatWpf.Core.ViewModel.Application
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
                // TODO: Fake a login...
                await System.Threading.Tasks.Task.Delay(1000);

                // OK successfully logged in... now get users data
                // TODO: Ask server for users info

                // TODO: Remove this with real information pulled from our database in future
                IoC.Base.IoC.Settings.Name = new TextEntryViewModel { Label = "Name", OriginalText = $"Luke Malpass {DateTime.Now.ToLocalTime()}" };
                IoC.Base.IoC.Settings.Username = new TextEntryViewModel { Label = "Username", OriginalText = "luke" };
                IoC.Base.IoC.Settings.Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
                IoC.Base.IoC.Settings.Email = new TextEntryViewModel { Label = "Email", OriginalText = "contact@angelsix.com" };

                // Go to chat page
                IoC.Base.IoC.Application.GoToPage(ApplicationPage.Chat);

                //var email = Email;

            });
        }

        public async System.Threading.Tasks.Task RegisterAsync()
        {
            // Go to register page?
            IoC.Base.IoC.Application.GoToPage(ApplicationPage.Register);

            await System.Threading.Tasks.Task.Delay(1);
        }
    }
}
