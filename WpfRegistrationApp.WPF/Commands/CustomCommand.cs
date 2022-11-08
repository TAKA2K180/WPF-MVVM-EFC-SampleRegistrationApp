using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework.Services;
using WpfRegistration.EntityFramework;

namespace WpfRegistrationApp.WPF.Commands
{
    public class CustomCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public CustomCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute==null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
