using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static RoutedCommand CreateHandlerWithBinding(Action action, Func<object, CanExecuteRoutedEventArgs, bool> canExecute, FrameworkElement assignablElement)
        {
            var cb = new CommandBinding(RoutedCommand, (sender, e) =>
            {
                action();
            } , (sender, e) =>
            {
                var target = e.Source as Control;
                e.CanExecute = target != null && canExecute(sender,e);
            });
            assignablElement.CommandBindings.Add(cb);
            return RoutedCommand;
        }

        public static RoutedCommand RoutedCommand =new RoutedCommand();
        public event EventHandler CanExecuteChanged;
    }
}