using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.ViewModel.Base;
using ChatWpf.ViewModel.Input;
using static ChatWpf.DI.DI;

namespace ChatWpf.ViewModel.Application
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

        public ICommand LoadCommand { get; set; }

        public SettingsViewModel()
        {
            // Create commands
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
            LogoutCommand = new RelayCommand(Logout);
            ClearUserDataCommand = new RelayCommand(ClearUserData);

            LoadCommand = new RelayCommand(async () => await LoadAsync());

            // TODO: Get from localization
            LogoutButtonText = "Logout";
        }

        public void Open()
        {
            ViewModelApplication.SettingsMenuVisible = true;
        }

        public void Close()
        {
            // Close settings menu
            ViewModelApplication.SettingsMenuVisible = false;
        }

        public void Logout()
        {
            // TODO: Confirm the user wants to logout

            // TODO: Clear any user data/cache

            // Clean all application level view models that contain
            // any information about the current user
            ClearUserData();

            // Go to login page
            ViewModelApplication.GoToPage(ApplicationPage.Login);
        }

        public void ClearUserData()
        {
            // Clear all view models containing the users info
            Name = null;
            Username = null;
            Password = null;
            Email = null;
        }

        public async System.Threading.Tasks.Task LoadAsync()
        {
            // Get the stored credentials
            var storedCredentials = await ClientDataStore.GetLoginCredentialsAsync();

            Name = new TextEntryViewModel { Label = "Name", OriginalText = $"{storedCredentials?.FirstName} {storedCredentials?.LastName}" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = storedCredentials?.Username };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = storedCredentials?.Email };
        }
    }
}
