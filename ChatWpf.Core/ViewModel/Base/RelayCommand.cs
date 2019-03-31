﻿using System;
using System.Windows.Input;

namespace ChatWpf.Core.ViewModel.Base
{
    public class RelayCommand : ICommand
    {
        private Action _mAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action)
        {
            _mAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mAction();
        }

    }
}