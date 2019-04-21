using System.Windows;
using ChatWpf.WPFViewModels;

namespace ChatWpf
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : System.Windows.Window
    {
        private DialogWindowViewModel mViewModel;

        public DialogWindowViewModel ViewModel
        {
            get => mViewModel;
            set
            {
                // Set new value
                mViewModel = value;

                // Update data context
                DataContext = mViewModel;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}
