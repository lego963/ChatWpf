using System.Threading.Tasks;
using ChatWpf.Core.ViewModel.Dialogs;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface IUIManager
    {
        System.Threading.Tasks.Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
