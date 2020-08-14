using System;
using System.Threading.Tasks;
using Whose_Turn.Context;

namespace Whose_Turn.Repositories
{
    public abstract class Repository<T, TU> : IRepository<T, TU> where T : class
    {
        private DatabaseContext _localContext { get; }
        public Repository(DatabaseContext context)
        {
            _localContext = context;
        }

        public virtual T GetById(TU id)
        {
            return _localContext.Find<T>(id);
        }

        public virtual Task<T> GetByIdAsync(TU id)
        {
            return _localContext.FindAsync<T>(id).AsTask();
        }

        public virtual Task AddNewAsync(T entity)
        {
            _localContext.Add(entity);
            return _localContext.SaveChangesAsync();
        }

        public virtual void AddNew(T entity)
        {
            _localContext.Add(entity);
            _localContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _localContext.Remove(entity);
            _localContext.SaveChanges();
        }

        public virtual Task DeleteAsync(T entity)
        {
            _localContext.Remove(entity);
            return _localContext.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            _localContext.Update(entity);
            _localContext.SaveChanges();
        }

        public virtual Task UpdateAsync(T entity)
        {
            _localContext.Update(entity);
            return _localContext.SaveChangesAsync();
        }
    }
}
