using ChatWpf.ViewModel.Dialogs;

namespace ChatWpf.DI.UI
{
    public interface IUIManager
    {
        System.Threading.Tasks.Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
