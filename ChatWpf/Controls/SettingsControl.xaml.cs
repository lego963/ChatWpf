using System.ComponentModel;
using System.Windows.Controls;
using ChatWpf.ViewModel.Application;

namespace ChatWpf.Controls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            DataContext = DesignerProperties.GetIsInDesignMode(this) ? new SettingsViewModel() : DI.Di.ViewModelSettings;
        }
    }
}
