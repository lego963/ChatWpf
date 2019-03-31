using System;
using System.Windows.Input;

namespace ChatWpf.Core
{
    public class RelayParameterizedCommand : ICommand
    {
        private Action<object> _mAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayParameterizedCommand(Action<object> action)
        {
            _mAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mAction(parameter);
        }
    }
}