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
    }
}
