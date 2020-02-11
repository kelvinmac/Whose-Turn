using System;
using Whose_Turn.Context.Entities;
using Whose_Turn.Repositories;

namespace Whose_Turn.Services
{
    public interface ITodoRepository : IRepository<Todo, Guid>
    {

    }
}
