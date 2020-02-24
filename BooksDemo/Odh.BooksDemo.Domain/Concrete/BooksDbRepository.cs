using System.Data.Entity;
using Odh.BooksDemo.Entities;
using Odh.BooksDemo.Domain.Abstract;

namespace Odh.BooksDemo.Domain.Concrete
{
    public class BooksDbRepository : GenericRepository<Book>, IBookRepository
    {
        public BooksDbRepository(DbContext context) : base(context) { }
    }
}
