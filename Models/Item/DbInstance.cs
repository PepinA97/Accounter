using System.ComponentModel.DataAnnotations;

namespace WpfApp1.Models.Item
{
    class DbInstance : ClassDetails
    {
        public int ID { get; set; }

        public int? Amount { get; set; }

        public DbInstance()
        {

        }

        public DbInstance(ClassDetails classDetails)
        {
            Name = classDetails.Name;
            Amount = classDetails.StartingAmount;
            Nutrition = classDetails.Nutrition;
        }
    }
}
