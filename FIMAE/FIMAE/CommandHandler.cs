using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIMAE
{
    public class CommandHandler : ICommand
    {
        private Action<object> _action;
        private Func<bool> _canExecute;
        public CommandHandler(Action<object> action, Func<bool> canExecute = null)
        {
            _action = action;
            if (canExecute != null)
            {
                _canExecute = canExecute;
            }
            else
            {
                _canExecute = () => { return true; };
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
