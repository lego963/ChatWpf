using System.Windows.Controls;

namespace ChatWpf.Controls
{
    /// <summary>
    /// Логика взаимодействия для SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            DataContext = Core.IoC.Base.IoC.Settings;
        }
    }
}
