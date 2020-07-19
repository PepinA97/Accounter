using System.Data.Entity;

namespace WpfApp1
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {

        }

        public DbSet<Models.Item.DbClass> DbClasses { get; set; }
        public DbSet<Models.Item.DbInstance> DbInstances { get; set; }
    }
}
