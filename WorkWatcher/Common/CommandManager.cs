using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Lorenzo.WorkWatcher.Common
{
    public class CommandManager : ICommandManager
    {
        public ICollection<RelayCommand> Commands { get; set; }

        public CommandManager()
        {
            Commands = new List<RelayCommand>();
        }

        public ICommand CreateCommand(Func<object, bool> canExecute = null, Action<object> execute = null)
        {
            var command = new RelayCommand(canExecute, execute);
            Commands.Add(command);

            return command;
        }

        public void RaiseAllCanExecute()
        {
            foreach (var command in Commands)
            {
                command.RaiseCanExecute();
            }
        }
    }
}
