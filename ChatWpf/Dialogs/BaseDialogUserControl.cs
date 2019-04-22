using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatWpf.ViewModel.Base;
using ChatWpf.ViewModel.Dialogs;
using ChatWpf.WPFViewModels;

namespace ChatWpf.Dialogs
{
    public class BaseDialogUserControl : UserControl
    {
        private readonly DialogWindow _dialogWindow;

        public ICommand CloseCommand { get; }

        public int WindowMinimumWidth { get; } = 250;

        public int WindowMinimumHeight { get; } = 100;

        public int TitleHeight { get; } = 30;

        public string Title { get; set; }

        public BaseDialogUserControl()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _dialogWindow = new DialogWindow();
                _dialogWindow.ViewModel = new DialogWindowViewModel(_dialogWindow);

                CloseCommand = new RelayCommand(() => _dialogWindow.Close());
            }
        }

        public Task ShowDialog<T>(T viewModel)
            where T : BaseDialogViewModel
        {
            var tcs = new TaskCompletionSource<bool>();

            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    _dialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    _dialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    _dialogWindow.ViewModel.TitleHeight = TitleHeight;
                    _dialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    _dialogWindow.ViewModel.Content = this;

                    DataContext = viewModel;

                    _dialogWindow.Owner = Application.Current.MainWindow;
                    _dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    _dialogWindow.ShowDialog();
                }
                finally
                {
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}
