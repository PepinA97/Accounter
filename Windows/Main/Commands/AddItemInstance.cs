using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfApp1.Windows.Main.Commands
{
    class AddItemInstance : ICommand
    {
        ViewModel VM;

        public AddItemInstance(ViewModel vm)
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
            Models.Item.DbClass dbClass = parameter as Models.Item.DbClass;

            using var db = new DatabaseContext();

            for (int i = 0; i < dbClass.BulkQuantity; i++)
            {
                db.DbInstances.Add(new Models.Item.DbInstance(dbClass));
            }

            db.SaveChanges();

            VM.OnPropertyChanged("ItemInstances");
        }
    }
}
