using System;
using System.Data.Entity;
using System.Linq;
using Odh.BooksDemo.Domain.Abstract;

namespace Odh.BooksDemo.Domain.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Add(entity);
            }
            else
            {
                dbEntityEntry.State = EntityState.Added;
            }
        }

        public void Delete(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Deleted)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            else
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return; // not found; assume already deleted.
            }

            Delete(entity);
        }

        public void Update(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
