using System.Windows;
using ChatWpf.Core.File;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.Logging.Core;
using ChatWpf.Core.Logging.Implementation;
using ChatWpf.Core.Task;
using ChatWpf.IoC;
using Dna;
using FileLogger = ChatWpf.Core.Logging.Implementation.FileLogger;

namespace ChatWpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            ApplicationSetup();

            // Log it
            Core.IoC.Base.IoC.Logger.Log("Application starting...", LogLevel.Debug);

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        private void ApplicationSetup()
        {
            Framework.Startup();

            // Setup IoC
            Core.IoC.Base.IoC.Setup();

            // Bind a logger
            Core.IoC.Base.IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLogFactory(
                new[]
                {
                // TODO: Add ApplicationSettings so we can set/edit a log location
                //       For now just log to the path where this application is running
                new FileLogger("Oldlog.txt"),
            }));

            // Add our task manager
            Core.IoC.Base.IoC.Kernel.Bind<ITaskManager>().ToConstant(new TaskManager());

            // Bind a file manager
            Core.IoC.Base.IoC.Kernel.Bind<IFileManager>().ToConstant(new FileManager());

            // Bind a UI Manager
            Core.IoC.Base.IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }
    }
}
