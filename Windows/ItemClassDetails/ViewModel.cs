using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Models.Item;

namespace WpfApp1.Windows.ItemClassDetails
{
    class ViewModel : INotifyPropertyChanged
    {
        #region On Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        View View;

        public RelayCommand Finish { get; set; }

        public DbClass DbClass { get; set; }

        public bool StartingAmountIsChecked { get; set; }

        public bool NutritionIsChecked { get; set; }

        public bool ComplexNutritionIsChecked { get; set; }

        bool FinishCanExecute()
        {
            return true;
        }

        void FinishExecute()
        {
            if ((String.IsNullOrEmpty(DbClass.Name)) || !(DbClass.BulkQuantity > 0))
            {
                return;
            }

            if (StartingAmountIsChecked && (!DbClass.StartingAmount.HasValue))
            {
                return;
            }

            if (NutritionIsChecked)
            {
                if (!(DbClass.Nutrition.Calories > 0))
                {
                    return;
                }

                if (ComplexNutritionIsChecked)
                {
                    if (!DbClass.Nutrition.HasComplexValues)
                    {
                        return;
                    }
                }
            }

            View.DialogResult = true;

            View.Close();
        }

        public bool ShowWindow(View view)
        {
            view.DataContext = this;
            View = view;

            return view.ShowDialog().GetValueOrDefault();
        }

        public ViewModel(DbClass dbClass)
        {
            Finish = new RelayCommand(FinishExecute, FinishCanExecute);

            DbClass = dbClass;
        }
    }
}
