using System.Windows.Input;

namespace SPZO.Commands
{
    public class RelayCommands : ICommand
    {
        //This class inherits ICommand interface. This allows me to implement commands as, save, add, remove, payment in viewmodel class and not directly in view class.
        private Action<object> execute; // Action takes one argument as object and do not return value
        private Func<object, bool> canExecute; // Func takes one arguemnt and return boolean value

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommands(Action<object> execute, Func<object, bool> canExecute = null) //Exceute represents what function will be executed. canExecute is optional parameter.
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
