using System.Threading.Tasks;

namespace Whose_Turn.Repositories
{
    public interface IRepository<T, TU> where T : class
    {
        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity"></param>
        void AddNew(T entity);
        /// <summary>
        /// Adds a new entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddNewAsync(T entity);

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
        /// <summary>
        /// Retrieves the entity with the given identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(TU id);
        /// <summary>
        /// Retrieves the entity with the given identifier asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TU id);

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Updates the given entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
    }
}