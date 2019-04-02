using System.Threading.Tasks;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.ViewModel.Dialogs;
using ChatWpf.Dialogs;

namespace ChatWpf.IoC
{
    public class UIManager : IUIManager
    {
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }
    }
}
