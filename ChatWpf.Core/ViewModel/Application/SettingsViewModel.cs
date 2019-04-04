using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.Input;

namespace ChatWpf.Core.ViewModel.Application
{
    public class SettingsViewModel : BaseViewModel
    {
        public TextEntryViewModel Name { get; set; }

        public TextEntryViewModel Username { get; set; }

        public PasswordEntryViewModel Password { get; set; }

        public TextEntryViewModel Email { get; set; }

        public string LogoutButtonText { get; set; }

        public ICommand OpenCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand LogoutCommand { get; set; }

        public ICommand ClearUserDataCommand { get; set; }

        public SettingsViewModel()
        {
            // Create commands
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
            LogoutCommand = new RelayCommand(Logout);
            ClearUserDataCommand = new RelayCommand(ClearUserData);

            // TODO: Remove this once th real back-end is ready
            Name = new TextEntryViewModel { Label = "Name", OriginalText = $"Luke Malpass" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "luke" };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "contact@angelsix.com" };

            // TODO: Get from localization
            LogoutButtonText = "Logout";
        }

        public void Open()
        {
            IoC.Base.IoC.Application.SettingsMenuVisible = true;
        }

        public void Close()
        {
            // Close settings menu
            IoC.Base.IoC.Application.SettingsMenuVisible = false;
        }

        public void Logout()
        {
            // TODO: Confirm the user wants to logout

            // TODO: Clear any user data/cache

            // Clean all application level view models that contain
            // any information about the current user
            ClearUserData();

            // Go to login page
            IoC.Base.IoC.Application.GoToPage(ApplicationPage.Login);
        }

        public void ClearUserData()
        {
            // Clear all view models containing the users info
            Name = null;
            Username = null;
            Password = null;
            Email = null;
        }
    }
}
