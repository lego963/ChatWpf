using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Input
{
    public class TextEntryViewModel : BaseViewModel
    {
        public string Label { get; set; }

        public string OriginalText { get; set; }

        public string EditedText { get; set; }

        public bool Editing { get; set; }

        public bool Working { get; set; }

        public Func<Task<bool>> CommitAction { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public TextEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        public void Edit()
        {
            EditedText = OriginalText;

            Editing = true;
        }

        public void Cancel()
        {
            Editing = false;
        }

        public void Save()
        {
            var result = default(bool);

            var currentSavedValue = OriginalText;

            RunCommandAsync(() => Working, async () =>
            {
                Editing = false;

                OriginalText = EditedText;

                result = CommitAction == null || await CommitAction();

            }).ContinueWith(t =>
            {
                // If we succeeded...
                // Nothing to do
                // If we fail...
                if (!result)
                {
                    // Restore original value
                    OriginalText = currentSavedValue;

                    // Go back into edit mode
                    Editing = true;
                }
            });
        }
    }
}
