using System;

namespace PS.UI.Shared.Commands
{
    public class DelegateCommand<T> : System.Windows.Input.ICommand where T : class
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        private readonly Action<Exception> _handleException;
        private readonly Action<bool> _setWorking;

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute, Action<Exception> handleException, Action<bool> setWorking)
        {
            _execute = execute;
            _canExecute = canExecute;
            _handleException = handleException;
            _setWorking = setWorking;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            try
            {
                _execute((T)parameter);
            }
            catch (Exception ex)
            {
                _setWorking(false);
                _handleException(ex);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}