using System.Threading.Tasks;
using System.Windows;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.DI;
using ChatWpf.DI;
using ChatWpf.Relational;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatWpf
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await ApplicationSetupAsync();

            FrameworkDI.Logger.LogDebugSource("Application starting...");

            Di.ViewModelApplication.GoToPage(
                await Di.ClientDataStore.HasCredentialsAsync() ?
                ApplicationPage.Chat :
                ApplicationPage.Login);

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        private async Task ApplicationSetupAsync()
        {
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .AddClientDataStore()
                .AddSynthesisViewModels()
                .AddSynthesisClientServices()
                .Build();

            await Di.ClientDataStore.EnsureDataStoreAsync();

            MonitorServerStatus();

            CoreDi.TaskManager.RunAndForget(Di.ViewModelSettings.LoadAsync);
        }

        private void MonitorServerStatus()
        {
            var httpWatcher = new HttpEndpointChecker(
                FrameworkDI.Configuration["SynthesisServer:HostUrl"],
                interval: 20000,
                logger: Framework.Provider.GetService<ILogger>(),
                stateChangedCallback: (result) =>
                {
                    Di.ViewModelApplication.ServerReachable = result;
                });
        }
    }
}
