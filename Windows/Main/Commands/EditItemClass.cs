using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfApp1.Windows.Main.Commands
{
    class EditItemClass : ICommand
    {
        ViewModel VM;

        public EditItemClass(ViewModel vm)
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

            ItemClassDetails.ViewModel viewModel = new ItemClassDetails.ViewModel(dbClass);

            if(viewModel.ShowWindow(new ItemClassDetails.View()))
            {
                using var db = new DatabaseContext();

                dbClass = viewModel.GetDbClass();

                db.DbClasses.Attach(dbClass);

                if(dbClass.ID == 0)
                {
                    db.Entry(dbClass).State = EntityState.Added;
                }
                else
                {
                    db.Entry(dbClass).State = EntityState.Modified;
                }

                db.SaveChanges();

                VM.OnPropertyChanged("ItemClasses");
            }
        }
    }
}
