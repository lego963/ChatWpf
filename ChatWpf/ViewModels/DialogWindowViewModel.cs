using System.Windows.Controls;

namespace ChatWpf.ViewModels
{
    public class DialogWindowViewModel : WindowViewModel
    {
        public string Title { get; set; }

        public Control Content { get; set; }

        public DialogWindowViewModel(System.Windows.Window window) : base(window)
        {
            // Make minimum size smaller
            WindowMinimumWidth = 250;
            WindowMinimumHeight = 100;

            // Make title bar smaller
            TitleHeight = 30;
        }
    }
}