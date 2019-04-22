using System.Threading.Tasks;
using ChatWpf.ViewModel.Dialogs;

namespace ChatWpf.DI.UI
{
    public interface IUiManager
    {
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
