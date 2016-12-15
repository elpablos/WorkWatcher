using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lorenzo.WorkWatcher.Common
{
    public class RelayCommand : ICommand
    {
        readonly Func<object, bool> canExecute;
        readonly Action<object> execute;

        public RelayCommand(Func<object, bool> canExecute = null, Action<object> execute = null)
        {
            this.canExecute = canExecute ?? (_ => true);
            this.execute = execute ?? (_ => { });
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void RaiseCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
