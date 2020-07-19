using System;
using System.Windows.Input;
using WpfApp1.Models.Item;

namespace WpfApp1.Windows.Main.Commands
{
    class CreateItemClass : ICommand
    {
        ViewModel VM;

        public CreateItemClass(ViewModel vm)
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
            DbClass dbClass = new DbClass();

            using var db = new DatabaseContext();

            db.DbClasses.Add(dbClass);

            VM.EditItemClass.Execute(dbClass);
        }
    }
}