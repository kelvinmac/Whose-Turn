using System.Threading.Tasks;

namespace Whose_Turn.Repositories
{
    public interface IRepository<T, TU> where T : class
    {
        void AddNew(T entity);
        Task AddNewAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        T GetById(TU id);
        Task<T> GetByIdAsync(TU id);
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}