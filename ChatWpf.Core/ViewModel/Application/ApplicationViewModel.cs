using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Application
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Chat;

        public BaseViewModel CurrentPageViewModel { get; set; }

        public bool SideMenuVisible { get; set; } = true;

        public bool SettingsMenuVisible { get; set; }

        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            SettingsMenuVisible = false;

            CurrentPageViewModel = viewModel;

            CurrentPage = page;

            OnPropertyChanged(nameof(CurrentPage));

            SideMenuVisible = page == ApplicationPage.Chat;
        }
    }
}
