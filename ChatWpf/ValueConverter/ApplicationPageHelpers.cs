using System.Diagnostics;
using ChatWpf.Core.DataModels;
using ChatWpf.Pages;
using ChatWpf.ViewModel.Application;
using ChatWpf.ViewModel.Chat.ChatMessage;

namespace ChatWpf.ValueConverter
{
    public static class ApplicationPageHelpers
    {
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            switch (page)
            {
                case ApplicationPage.Login:
                    return new LoginPage(viewModel as LoginViewModel);

                case ApplicationPage.Register:
                    return new RegisterPage(viewModel as RegisterViewModel);

                case ApplicationPage.Chat:
                    return new ChatPage(viewModel as ChatMessageListViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            if (page is ChatPage)
                return ApplicationPage.Chat;

            if (page is LoginPage)
                return ApplicationPage.Login;

            if (page is RegisterPage)
                return ApplicationPage.Register;

            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
