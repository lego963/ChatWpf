using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.ViewModel.Application;
using Ninject;

namespace ChatWpf.Core.IoC.Base
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static IUIManager UI => IoC.Get<IUIManager>();

        public static ILogFactory Logger => Get<ILogFactory>();

        public static IFileManager File => Get<IFileManager>();

        public static ITaskManager Task => Get<ITaskManager>();

        public static ApplicationViewModel Application => IoC.Get<ApplicationViewModel>();

        public static SettingsViewModel Settings => IoC.Get<SettingsViewModel>();

        public static void Setup()
        {
            // Bind all required view models
            BindViewModels();
        }

        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            // Bind to a single instance of Settings view model
            Kernel.Bind<SettingsViewModel>().ToConstant(new SettingsViewModel());
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
