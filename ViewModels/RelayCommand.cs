using System.Windows.Input;

namespace WinPilot.ViewModels;

/// <summary>
/// RelayCommand is a simple implementation of ICommand.
/// </summary>
public class RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null) : ICommand
{
    private readonly Action<object?> execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Predicate<object?>? canExecute = canExecute;

    public bool CanExecute(object? parameter) => canExecute == null || canExecute(parameter);
    public void Execute(object? parameter) => execute(parameter);
    
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}