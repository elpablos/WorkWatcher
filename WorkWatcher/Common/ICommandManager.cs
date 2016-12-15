using System;
using System.Windows.Input;

namespace Lorenzo.WorkWatcher.Common
{
    public interface ICommandManager
    {
        ICommand CreateCommand(Func<object, bool> canExecute = null, Action<object> execute = null);

        void RaiseAllCanExecute();
    }
}