using System.ComponentModel.DataAnnotations;

namespace WpfApp1.Models.Item
{
    class DbClass : ClassDetails
    {
        public int ID { get; set; }

        public int BulkQuantity { get; set; }

        public DbClass()
        {

        }

        public DbClass(string name)
        {
            Name = name;

            BulkQuantity = 1; // Is overwritten by EF setters
        }
    }
}
