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
    }
}
