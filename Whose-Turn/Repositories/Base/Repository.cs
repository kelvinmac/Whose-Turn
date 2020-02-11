using System;
using System.Threading.Tasks;
using Whose_Turn.Context;

namespace Whose_Turn.Repositories
{
    public abstract class Repository<T, TU> where T : class
    {
        private DatabaseContext _localContext { get; }
        public Repository(DatabaseContext context)
        {
            _localContext = context;
        }

        public T GetById(TU id)
        {
            return _localContext.Find<T>(id);
        }

        public Task<T> GetByIdAsync(TU id)
        {
            return _localContext.FindAsync<T>(id).AsTask();
        }

        public Task AddNewAsync(T entity)
        {
            _localContext.Add(entity);
            return _localContext.SaveChangesAsync();
        }

        public void AddNew(T entity)
        {
            _localContext.Add(entity);
            _localContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _localContext.Remove(entity);
            _localContext.SaveChanges();
        }

        public Task DeleteAsync(T entity)
        {
            _localContext.Remove(entity);
            return _localContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _localContext.Update(entity);
            _localContext.SaveChanges();
        }

        public Task UpdateAsync(T entity)
        {
            _localContext.Update(entity);
            return _localContext.SaveChangesAsync();
        }
    }
}
