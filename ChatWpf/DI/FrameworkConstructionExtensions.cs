﻿using ChatWpf.Core.File;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.Task;
using ChatWpf.DI.UI;
using ChatWpf.ViewModel.Application;
using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.DI
{
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for Fasetto Word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddChatViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of Settings view model
            construction.Services.AddSingleton<SettingsViewModel>();

            // Return the construction for chaining
            return construction;
        }

        /// <summary>
        /// Injects the Fasetto Word client application services needed
        /// for the Fasetto Word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddChatClientServices(this FrameworkConstruction construction)
        {

            // Add our task manager
            construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            // Bind a file manager
            construction.Services.AddTransient<IFileManager, BaseFileManager>();

            // Bind a UI Manager
            construction.Services.AddTransient<IUIManager, UIManager>();

            // Return the construction for chaining
            return construction;
        }
    }
}
