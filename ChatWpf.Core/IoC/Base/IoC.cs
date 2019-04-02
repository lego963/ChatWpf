using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.ViewModel;
using ChatWpf.Core.ViewModel.Application;
using Ninject;

namespace ChatWpf.Core.IoC.Base
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static IUIManager UI => IoC.Get<IUIManager>();

        public static ApplicationViewModel Application => IoC.Get<ApplicationViewModel>();

        public static SettingsViewModel Settings => IoC.Get<SettingsViewModel>();

        public static void Setup()
        {
            BindViewModels();
        }

        private static void BindViewModels()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            Kernel.Bind<SettingsViewModel>().ToConstant(new SettingsViewModel());
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
