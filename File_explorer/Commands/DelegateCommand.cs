using System;
using System.Windows.Input;

namespace File_explorer.ViewModels
{
    partial class ApplicationViewModel
    {
        private class DelegateCommand : ICommand
        {
            private Action<object> _open;

            public DelegateCommand(Action<object> open)
            {
                _open = open;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _open?.Invoke(parameter);
            }
        }
    }
}
