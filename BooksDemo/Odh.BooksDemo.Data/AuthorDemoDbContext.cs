using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Odh.BooksDemo.Entities;

namespace Odh.BooksDemo.Data
{
    public class AuthorDemoDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
               .Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new ElmahErrorConfiguration());
        }

    }
}
