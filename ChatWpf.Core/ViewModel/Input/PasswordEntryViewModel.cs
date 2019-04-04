using System.Security;
using System.Windows.Input;
using ChatWpf.Core.Security;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.Dialogs;

namespace ChatWpf.Core.ViewModel.Input
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
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

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
            var storedPassword = "Testing";

            if (storedPassword != CurrentPassword.Unsecure())
            {
                IoC.Base.IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Wrong password",
                    Message = "The current password is invalid"
                });

                return;
            }

            if (NewPassword.Unsecure() != ConfirmPassword.Unsecure())
            {
                IoC.Base.IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Password mismatch",
                    Message = "The new and confirm password do not match"
                });

                return;
            }

            if (NewPassword.Unsecure().Length == 0)
            {
                IoC.Base.IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Password too short",
                    Message = "You must enter a password!"
                });

                return;
            }
            CurrentPassword = new SecureString();
            foreach (var c in NewPassword.Unsecure().ToCharArray())
                CurrentPassword.AppendChar(c);

            Editing = false;
        }
    }
}