using System.Data.Entity;

namespace lab5
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext():base("DefaultConnection")
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
