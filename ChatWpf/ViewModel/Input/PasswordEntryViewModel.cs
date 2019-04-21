using System.Security;
using System.Windows.Input;
using ChatWpf.Core.Security;
using ChatWpf.ViewModel.Base;
using ChatWpf.ViewModel.Dialogs;
using static ChatWpf.DI.DI;

namespace ChatWpf.ViewModel.Input
{
    public class PasswordEntryViewModel : BaseViewModel
    {
        public string Label { get; set; }

        public string FakePassword { get; set; }

        public string CurrentPasswordHintText { get; set; }

        public string NewPasswordHintText { get; set; }

        public string ConfirmPasswordHintText { get; set; }

        public SecureString CurrentPassword { get; set; }

        public SecureString NewPassword { get; set; }

        public SecureString ConfirmPassword { get; set; }

        public bool Editing { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public PasswordEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

            // Set default hints
            // TODO: Replace with localization text
            CurrentPasswordHintText = "Current Password";
            NewPasswordHintText = "New Password";
            ConfirmPasswordHintText = "Confirm Password";
        }

        public void Edit()
        {
            // Clear all password
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();

            // Go into edit mode
            Editing = true;
        }

        public void Cancel()
        {
            Editing = false;
        }

        public void Save()
        {
            // Make sure current password is correct
            // TODO: This will come from the real back-end store of this users password
            //       or via asking the web server to confirm it
            var storedPassword = "Testing";

            // Confirm current password is a match
            // NOTE: Typically this isn't done here, it's done on the server
            if (storedPassword != CurrentPassword.Unsecure())
            {
                // Let user know
                UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Wrong password",
                    Message = "The current password is invalid"
                });

                return;
            }

            // Now check that the new and confirm password match
            if (NewPassword.Unsecure() != ConfirmPassword.Unsecure())
            {
                // Let user know
                UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Password mismatch",
                    Message = "The new and confirm password do not match"
                });

                return;
            }

            // Check we actually have a password
            if (NewPassword.Unsecure().Length == 0)
            {
                // Let user know
                UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Password too short",
                    Message = "You must enter a password!"
                });

                return;
            }

            // Set the edited password to the current value
            CurrentPassword = new SecureString();
            foreach (var c in NewPassword.Unsecure().ToCharArray())
                CurrentPassword.AppendChar(c);

            Editing = false;
        }
    }
}
