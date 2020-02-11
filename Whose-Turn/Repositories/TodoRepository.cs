using System;
using Whose_Turn.Context;
using Whose_Turn.Context.Entities;
using Whose_Turn.Services;

namespace Whose_Turn.Repositories
{
    public class TodoRepository : Repository<Todo, Guid>, ITodoRepository
    {
        public TodoRepository(DatabaseContext context)
            :base(context)
        {
        }


    }
}
