using ChatWpf.Core.DI.Interfaces;
using ChatWpf.Core.File;
using ChatWpf.Core.Task;
using ChatWpf.DI.UI;
using ChatWpf.ViewModel.Application;
using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.DI
{
    public static class FrameworkConstructionExtensions
    {
        public static FrameworkConstruction AddSynthesisViewModels(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<ApplicationViewModel>();

            construction.Services.AddSingleton<SettingsViewModel>();

            return construction;
        }

        public static FrameworkConstruction AddSynthesisClientServices(this FrameworkConstruction construction)
        {
            construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            construction.Services.AddTransient<IFileManager, BaseFileManager>();

            construction.Services.AddTransient<IUiManager, UiManager>();

            return construction;
        }
    }
}
