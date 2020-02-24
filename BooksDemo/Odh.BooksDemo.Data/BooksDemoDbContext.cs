using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Odh.BooksDemo.Entities;

namespace Odh.BooksDemo.Data
{
    public class BooksDemoDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
               .Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new ElmahErrorConfiguration());
        }

    }
}
