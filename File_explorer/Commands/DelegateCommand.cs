using System;
using System.Windows.Input;

namespace File_explorer.ViewModels
{
    partial class ApplicationViewModel
    {
        public class DelegateCommand : ICommand
        {
            private Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            public DelegateCommand(Action<object> execute, Predicate<Object> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute.Invoke(parameter);

            public void Execute(object parameter)
            {
                _execute?.Invoke(parameter);
            }

            public event EventHandler CanExecuteChanged;

            public void RaiseCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
