using ChatWpf.Core.ApiModels;
using ChatWpf.Core.DataModels;
using ChatWpf.ViewModel.Base;
using static ChatWpf.DI.DI;

namespace ChatWpf.ViewModel.Application
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        public BaseViewModel CurrentPageViewModel { get; set; }

        public bool SideMenuVisible { get; set; } = false;

        public bool SettingsMenuVisible { get; set; }

        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Always hide settings page if we are changing pages
            SettingsMenuVisible = false;

            // Set the view model
            CurrentPageViewModel = viewModel;

            // Set the current page
            CurrentPage = page;

            // Fire off a CurrentPage changed event
            OnPropertyChanged(nameof(CurrentPage));

            // Show side menu or not?
            SideMenuVisible = page == ApplicationPage.Chat;

        }

        public async System.Threading.Tasks.Task HandleSuccessfulLoginAsync(LoginResultApiModel loginResult)
        {
            // Store this in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(new LoginCredentialsDataModel
            {
                Email = loginResult.Email,
                FirstName = loginResult.FirstName,
                LastName = loginResult.LastName,
                Username = loginResult.Username,
                Token = loginResult.Token
            });

            // Load new settings
            await ViewModelSettings.LoadAsync();

            // Go to chat page
            ViewModelApplication.GoToPage(ApplicationPage.Chat);
        }
    }
}
