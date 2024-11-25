using System;
using System.Windows.Input;

namespace PersonalOrganizer.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        // Konstruktor RelayCommand
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); // Zapewnienie, że _execute nie jest null
            _canExecute = canExecute; // Pozwalamy na null dla canExecute
        }

        // Metoda sprawdzająca, czy polecenie może zostać wykonane
        public bool CanExecute(object parameter)
        {
            // Jeśli _canExecute jest null, traktujemy jako zawsze wykonane
            return _canExecute == null || _canExecute(parameter);
        }

        // Metoda wykonująca polecenie
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // Podnosi zdarzenie, które informuje o zmianie w wyniku CanExecute
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
