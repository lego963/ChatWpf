using System;
using System.Windows.Input;

namespace ChatWpf.Core.ViewModel.Base
{
    public class RelayParameterizedCommand : ICommand
    {
        private Action<object> mAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayParameterizedCommand(Action<object> action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction(parameter);
        }
    }
}
