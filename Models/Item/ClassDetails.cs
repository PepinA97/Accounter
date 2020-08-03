using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models.Item
{
    abstract class ClassDetails : INotifyPropertyChanged
    {
        #region On Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public string Name { get; set; }

        public int? StartingAmount { get; set; }

        public Nutrition Nutrition { get; set; }

        public ClassDetails()
        {
            Nutrition = new Nutrition();
        }

        public bool HasAmount
        {
            get
            {
                return StartingAmount.HasValue;
            }
        }
    }

    class Nutrition : ObservableObject
    {
        public int? Calories { get; set; }

        public int? Protein { get; set; }
        public int? Fat { get; set; }
        public int? Carbohydrates { get; set; }

        public Nutrition()
        {

        }

        public bool HasComplexValues
        {
            get
            {
                return (Protein.HasValue || Fat.HasValue || Carbohydrates.HasValue);
            }
        }
    }
}
