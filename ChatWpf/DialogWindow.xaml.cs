using ChatWpf.ViewModels;

namespace ChatWpf
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : System.Windows.Window
    {
        private DialogWindowViewModel mViewModel;

        public DialogWindowViewModel ViewModel
        {
            get => mViewModel;
            set
            {
                mViewModel = value;

                DataContext = mViewModel;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}