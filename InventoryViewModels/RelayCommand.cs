using System;
using System.Windows.Input;

namespace InventoryViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> execute;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="execute">Command execute action.</param>
        /// <param name="canExecute">Can execute function pointer.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///     Check whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Parameters</param>
        /// <returns>Returns whether the command can be executed.</returns>
        public bool CanExecute(object parameter) {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        ///     Execute the command.
        /// </summary>
        /// <param name="parameter">Command parameters</param>
        public void Execute(object parameter) {
            execute(parameter);
        }
    }

}
