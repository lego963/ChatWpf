using ChatWpf.Core.ViewModel;
using ChatWpf.Core.ViewModel.Application;

namespace ChatWpf.ViewModels
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        public static ApplicationViewModel ApplicationViewModel => Core.IoC.Base.IoC.Application;

        public static SettingsViewModel SettingsViewModel => Core.IoC.Base.IoC.Settings;
    }
}
