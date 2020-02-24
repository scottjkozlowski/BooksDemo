using System.Linq;

namespace Odh.BooksDemo.Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Update(T entity);
    }
}
