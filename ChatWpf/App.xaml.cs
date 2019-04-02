using System.Windows;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.IoC;

namespace ChatWpf
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationSetup();

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        private void ApplicationSetup()
        {
            Core.IoC.Base.IoC.Setup();
            Core.IoC.Base.IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }
    }
}
