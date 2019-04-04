using System.Windows;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.IoC;

namespace ChatWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            ApplicationSetup();

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private void ApplicationSetup()
        {
            // Setup IoC
            Core.IoC.Base.IoC.Setup();

            // Bind a UI Manager
            Core.IoC.Base.IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }
    }
}
