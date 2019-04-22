using System.Threading.Tasks;
using System.Windows;
using ChatWpf.Dialogs;
using ChatWpf.ViewModel.Dialogs;

namespace ChatWpf.DI.UI
{
    public class UiManager : IUiManager
    {
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            var tcs = new TaskCompletionSource<bool>();

            Application.Current.Dispatcher.Invoke(async () =>
            {
                try
                {
                    await new DialogMessageBox().ShowDialog(viewModel);
                }
                finally
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}
