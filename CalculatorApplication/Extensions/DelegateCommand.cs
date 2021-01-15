using System;
using System.Windows.Input;

namespace CalculatorApplication.Extensions
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        public DelegateCommand(Action<object> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(_action));
        }

        public DelegateCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            _action = action ?? throw new ArgumentNullException(nameof(_action));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(_canExecute));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _action(parameter);
            }
        }
    }
}
