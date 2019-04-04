using ChatWpf.Core.ViewModel.Input;

namespace ChatWpf.Core.ViewModel.Application.Design
{
    public class SettingsDesignModel : SettingsViewModel
    {
        public static SettingsDesignModel Instance => new SettingsDesignModel();

        public SettingsDesignModel()
        {
            Name = new TextEntryViewModel { Label = "Name", OriginalText = "Luke Malpass" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "luke" };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "contact@angelsix.com" };
        }
    }
}
