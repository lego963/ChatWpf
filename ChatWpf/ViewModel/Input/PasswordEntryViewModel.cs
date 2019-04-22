using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.ViewModel.Base;

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

        public bool Working { get; set; }

        public Func<Task<bool>> CommitAction { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public PasswordEntryViewModel()
        {
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
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();

            Editing = true;
        }

        public void Cancel()
        {
            Editing = false;
        }

        public void Save()
        {
            var result = default(bool);

            RunCommandAsync(() => Working, async () =>
            {
                Editing = false;

                result = CommitAction == null ? true : await CommitAction();

            }).ContinueWith(t =>
            {
                // If we succeeded...
                // Nothing to do
                // If we fail...
                if (!result)
                {
                    // Go back into edit mode
                    Editing = true;
                }
            });
        }
    }
}
