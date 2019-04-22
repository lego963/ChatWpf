using ChatWpf.ViewModel.Input;

namespace ChatWpf.ViewModel.Application.Design
{
    public class SettingsDesignModel : SettingsViewModel
    {
        public static SettingsDesignModel Instance => new SettingsDesignModel();

        public SettingsDesignModel()
        {
            FirstName = new TextEntryViewModel { Label = "Fist Name", OriginalText = "Rodion" };
            LastName = new TextEntryViewModel { Label = "Last Name", OriginalText = "Gyrbu" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "rgyrbu" };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "fpsoff@outlook.com" };
        }

    }
}
