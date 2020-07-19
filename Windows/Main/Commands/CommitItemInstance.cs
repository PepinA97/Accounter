using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfApp1.Windows.Main.Commands
{
    class CommitItemInstance : ICommand
    {
        ViewModel VM;

        public CommitItemInstance(ViewModel vm)
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
            Models.Item.DbInstance dbInstance = parameter as Models.Item.DbInstance;

            using var db = new DatabaseContext();

            db.DbInstances.Attach(dbInstance);

            db.DbInstances.Remove(dbInstance);

            db.SaveChanges();

            VM.OnPropertyChanged("ItemInstances");
        }
    }
}
