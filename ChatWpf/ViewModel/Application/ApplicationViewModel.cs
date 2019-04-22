using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.Core.ApiModels.UserProfile;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.DI;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Application
{
    public class ApplicationViewModel : BaseViewModel
    {
        private bool _settingsMenuVisible;

        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        public BaseViewModel CurrentPageViewModel { get; set; }

        public bool SideMenuVisible { get; set; }

        public bool SettingsMenuVisible
        {
            get => _settingsMenuVisible;
            set
            {
                if (_settingsMenuVisible == value)
                    return;

                _settingsMenuVisible = value;

                if (value)
                    CoreDi.TaskManager.RunAndForget(DI.Di.ViewModelSettings.LoadAsync);
            }
        }

        public SideMenuContent CurrentSideMenuContent { get; set; } = SideMenuContent.Chat;

        public bool ServerReachable { get; set; } = true;

        public ICommand OpenChatCommand { get; set; }

        public ICommand OpenContactsCommand { get; set; }

        public ICommand OpenMediaCommand { get; set; }

        public ApplicationViewModel()
        {
            OpenChatCommand = new RelayCommand(OpenChat);
            OpenContactsCommand = new RelayCommand(OpenContacts);
            OpenMediaCommand = new RelayCommand(OpenMedia);
        }

        public void OpenChat()
        {
            CurrentSideMenuContent = SideMenuContent.Chat;
        }

        public void OpenContacts()
        {
            CurrentSideMenuContent = SideMenuContent.Contacts;
        }

        public void OpenMedia()
        {
            CurrentSideMenuContent = SideMenuContent.Media;
        }

        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            SettingsMenuVisible = false;

            CurrentPageViewModel = viewModel;

            var different = CurrentPage != page;

            CurrentPage = page;

            if (!different)
                OnPropertyChanged(nameof(CurrentPage));

            SideMenuVisible = page == ApplicationPage.Chat;

        }

        public async Task HandleSuccessfulLoginAsync(UserProfileDetailsApiModel loginResult)
        {
            await DI.Di.ClientDataStore.SaveLoginCredentialsAsync(loginResult.ToLoginCredentialsDataModel());

            await DI.Di.ViewModelSettings.LoadAsync();

            DI.Di.ViewModelApplication.GoToPage(ApplicationPage.Chat);
        }
    }
}
