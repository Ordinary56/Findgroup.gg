using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Core
{
    public class RelayCommand(Action<object?> execute, Predicate<object?>? canexecute ) : ICommand
    {
        private Action<object?> _execute = execute;
        private Predicate<object?>? _canexecute = canexecute;

        public RelayCommand(Action<object?> execute) : this(execute,null) { }
        public event EventHandler? CanExecuteChanged 
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        public bool CanExecute(object? parameter)
        {
            return _canexecute == null || _canexecute(parameter);
        }

        public void Execute(object? parameter)
        {
            ArgumentNullException.ThrowIfNull(_execute, nameof(_execute));
            _execute(parameter);
        }
    }
}
