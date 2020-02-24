using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odh.BooksDemo.Data;
using Odh.BooksDemo.Domain.Abstract;

namespace Odh.BooksDemo.Domain.Concrete
{
    public class BooksDemoUow : IBooksDemoUow
    {
        private readonly BooksDemoDbContext _context;
        public BooksDemoUow()
        {
            _context = new BooksDemoDbContext();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IBookRepository BookRepository
        {
            get { return new BooksDbRepository(_context); }
        }
    }
}
