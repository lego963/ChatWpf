﻿using System.Threading.Tasks;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.ViewModel.Dialogs;
using ChatWpf.Dialogs;

namespace ChatWpf.IoC
{
    /// <summary>
    /// The applications implementation of the <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns></returns>
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }
    }
}
