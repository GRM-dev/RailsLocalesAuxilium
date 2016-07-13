using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RailsLocalesAuxilium.ProjectPages;

namespace RailsLocalesAuxilium.Sources
{
    public class CommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly Func<object, CanExecuteRoutedEventArgs, bool> _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = delegate { return canExecute; };
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _canExecute.Invoke(sender, e);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(null, null);
        }

        public void Execute(object parameter)
        {
            _action();
        }

        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _action();
        }

        public static RoutedCommand CreateHandlerWithBinding(Action action, Func<object, CanExecuteRoutedEventArgs, bool> canExecute, FrameworkElement assignableElement, object parameters = null)
        {
            RoutedCommand routedCommand = null;
            if (parameters != null)
            {
                var paramType= parameters.GetType();
                if (paramType.IsClass)
                {
                    if (parameters is Type)
                    {
                        paramType = parameters as Type;
                        if (paramType.IsSubclassOf(typeof (ProjectPage)))
                        {
                            routedCommand = new RoutedCommand(paramType.Name, typeof (MainPage));
                        }
                    }
                }
            }
            if (routedCommand == null)
            {
                routedCommand = new RoutedCommand();
            }
            var cb = new CommandBinding(routedCommand, (sender, e) =>
            {
                action();
            }, (sender, e) =>
           {
               var target = e.Source as Control;
               e.CanExecute = target != null && canExecute(sender, e);
           });
            assignableElement.CommandBindings.Add(cb);

            return routedCommand;
        }

        public event EventHandler CanExecuteChanged;
    }
}