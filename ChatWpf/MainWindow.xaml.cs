using System;
using ChatWpf.WPFViewModels;

namespace ChatWpf
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new WindowViewModel(this);
        }

        private void AppWindow_Deactivated(object sender, EventArgs e)
        {
            ((WindowViewModel) DataContext).DimmableOverlayVisible = true;
        }

        private void AppWindow_Activated(object sender, EventArgs e)
        {
            ((WindowViewModel) DataContext).DimmableOverlayVisible = false;
        }
    }
}
