using System.Data.Entity;
using Odh.BooksDemo.Entities;
using Odh.BooksDemo.Domain.Abstract;

namespace Odh.BooksDemo.Domain.Concrete
{
    public class AuthorDbRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorDbRepository(DbContext context) : base(context) { }
    }
}
