using System.Threading.Tasks;
using ChatWpf.Core.ViewModel.Dialogs;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface IUIManager
    {
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
