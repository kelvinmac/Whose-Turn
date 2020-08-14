using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whose_Turn.Context.Entities;
using Whose_Turn.Repositories;

namespace Whose_Turn.Services
{
    public interface ITodoRepository : IRepository<Todo, Guid>
    {
        /// <summary>
        /// Retrieves all the active todo's primary keys
        /// </summary>
        /// <param name="userId"> The user's ID </param>
        /// <returns>A collection of Todos Id</returns>
        public Task<List<Guid>> GetUserActiveTodoIdentifiersAsync(Guid userId);
        
        /// <summary>
        /// Retrieves all the active todo's entities
        /// </summary>
        /// <param name="userId"> The user Id</param>
        /// <returns>A collection of Todos</returns>
        public Task<List<Todo>> GetActiveUserTodosAsync(Guid userId);
    }
}