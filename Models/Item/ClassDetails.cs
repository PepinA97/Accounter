using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace WpfApp1.Models.Item
{
    abstract class ClassDetails
    {
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

    class Nutrition
    {
        public int Calories { get; set; }

        public int? Protein { get; set; }
        public int? Fat { get; set; }
        public int? Carbohydrates { get; set; }

        public Nutrition()
        {
            Calories = 0;
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
