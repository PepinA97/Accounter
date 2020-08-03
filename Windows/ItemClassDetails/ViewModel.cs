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
        int DbClassID;

        public RelayCommand Finish { get; set; }

        bool _StartingAmountIsChecked;
        public bool StartingAmountIsChecked
        {
            get
            {
                return _StartingAmountIsChecked;
            }
            set
            {
                if (!value)
                {
                    StartingAmount = null;

                    OnPropertyChanged("StartingAmount");
                }

                _StartingAmountIsChecked = value;

                Finish.RaiseCanExecuteChanged();
            }
        }

        bool _NutritionIsChecked;
        public bool NutritionIsChecked
        {
            get
            {
                return _NutritionIsChecked;
            }
            set
            {
                if (!value)
                {
                    Calories = null;

                    OnPropertyChanged("Calories");

                    ComplexNutritionIsChecked = false;
                    OnPropertyChanged("ComplexNutritionIsChecked");
                }

                _NutritionIsChecked = value;

                Finish.RaiseCanExecuteChanged();
            }
        }

        bool _ComplexNutritionIsChecked;
        public bool ComplexNutritionIsChecked
        {
            get
            {
                return _ComplexNutritionIsChecked;
            }
            set
            {
                if (!value)
                {
                    Protein = null;
                    Fat = null;
                    Carbohydrates = null;

                    OnPropertyChanged("Protein");
                    OnPropertyChanged("Fat");
                    OnPropertyChanged("Carbohydrates");
                }

                _ComplexNutritionIsChecked = value;

                Finish.RaiseCanExecuteChanged();
            }
        }

        bool FinishCanExecute()
        {
            if (String.IsNullOrEmpty(Name))
            {
                return false;
            }

            if(BulkQuantity == 0)
            {
                return false;
            }

            if (StartingAmountIsChecked)
            {
                if (StartingAmount.GetValueOrDefault() == 0)
                {
                    return false;
                }
            }

            if (NutritionIsChecked)
            {
                if (Calories.GetValueOrDefault() == 0)
                {
                    return false;
                }

                if (ComplexNutritionIsChecked)
                {
                    if((Protein.GetValueOrDefault() == 0) || (Fat.GetValueOrDefault() == 0) || (Carbohydrates.GetValueOrDefault() == 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        void FinishExecute()
        {
            View.DialogResult = true;

            View.Close();
        }

        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        int _BulkQuantity;
        public int BulkQuantity
        {
            get
            {
                return _BulkQuantity;
            }
            set
            {
                _BulkQuantity = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        int? _StartingAmount;
        public int? StartingAmount
        {
            get
            {
                return _StartingAmount;
            }
            set
            {
                _StartingAmount = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        int? _Calories;
        public int? Calories
        {
            get
            {
                return _Calories;
            }
            set
            {
                _Calories = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        int? _Protein;
        public int? Protein
        {
            get
            {
                return _Protein;
            }
            set
            {
                _Protein = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        int? _Fat;
        public int? Fat
        {
            get
            {
                return _Fat;
            }
            set
            {
                _Fat = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        int? _Carbohydrates;
        public int? Carbohydrates
        {
            get
            {
                return _Carbohydrates;
            }
            set
            {
                _Carbohydrates = value;
                Finish.RaiseCanExecuteChanged();
            }
        }

        public DbClass GetDbClass()
        {
            DbClass dbClass = new DbClass();

            dbClass.ID = DbClassID;

            dbClass.Name = Name;
            dbClass.BulkQuantity = BulkQuantity;

            dbClass.StartingAmount = StartingAmount;

            dbClass.Nutrition.Calories = Calories;

            dbClass.Nutrition.Protein = Protein;
            dbClass.Nutrition.Fat = Fat;
            dbClass.Nutrition.Carbohydrates = Carbohydrates;

            return dbClass;
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

            DbClassID = dbClass.ID;

            Name = dbClass.Name;
            BulkQuantity = dbClass.BulkQuantity;

            StartingAmount = dbClass.StartingAmount;

            Calories = dbClass.Nutrition.Calories;

            Protein = dbClass.Nutrition.Protein;
            Fat = dbClass.Nutrition.Fat;
            Carbohydrates = dbClass.Nutrition.Carbohydrates;
        }
    }
}
