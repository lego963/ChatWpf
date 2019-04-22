using ChatWpf.WPFViewModels;

namespace ChatWpf
{
    public partial class DialogWindow : System.Windows.Window
    {
        private DialogWindowViewModel _viewModel;

        public DialogWindowViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;

                DataContext = _viewModel;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}
