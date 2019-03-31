using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        public bool SideMenuVisible { get; set; } = false;

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;

            SideMenuVisible = page == ApplicationPage.Chat;
        }
    }
}
