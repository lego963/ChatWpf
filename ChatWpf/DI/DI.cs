using ChatWpf.Core.DI.Interfaces;
using ChatWpf.DI.UI;
using ChatWpf.ViewModel.Application;
using Dna;

namespace ChatWpf.DI
{
    public static class Di
    {
        public static IUiManager Ui => Framework.Service<IUiManager>();

        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();

        public static SettingsViewModel ViewModelSettings => Framework.Service<SettingsViewModel>();

        public static IClientDataStore ClientDataStore => Framework.Service<IClientDataStore>();
    }
}
