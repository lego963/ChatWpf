using System.Windows.Input;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Input
{
    public class TextEntryViewModel : BaseViewModel
    {
        public string Label { get; set; }

        public string OriginalText { get; set; }

        public string EditedText { get; set; }

        public bool Editing { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public TextEntryViewModel()
        {
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
            // TODO: Save content
            OriginalText = EditedText;

            Editing = false;
        }
    }
}
