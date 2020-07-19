using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.Windows.Main.Commands
{
    class DeleteItemClass : ICommand
    {
        ViewModel VM;

        public DeleteItemClass(ViewModel vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using var db = new DatabaseContext();

            db.DbClasses.Attach(VM.SelectedItemClass);

            db.DbClasses.Remove(VM.SelectedItemClass);

            db.SaveChanges();

            VM.OnPropertyChanged("ItemClasses");
        }
    }
}
