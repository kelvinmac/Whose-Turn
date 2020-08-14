using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Whose_Turn.Context;
using Whose_Turn.Context.Entities;
using Whose_Turn.Services;

namespace Whose_Turn.Repositories
{
    public class TodoRepository : Repository<Todo, Guid>, ITodoRepository
    {
        private readonly DatabaseContext _dbContext;

        public TodoRepository(DatabaseContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public override Todo GetById(Guid id)
        {
            return _dbContext.Todos.Where(t => t.Id == id)
                .Include(t => t.Preferences)
                .FirstOrDefault();
        }

        public override Task<Todo> GetByIdAsync(Guid id)
        {
            return _dbContext.Todos.Where(t => t.Id == id)
                .Include(t => t.Preferences)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Guid>> GetUserActiveTodoIdentifiersAsync(Guid userId)
        {
            var todos = await GetActiveUserTodosAsync(userId);

            return todos
                .Select(t => t.Id)
                .ToList();
        }

        public async Task<List<Todo>> GetActiveUserTodosAsync(Guid userId)
        {
            var householdTodos = await _dbContext.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.MyHouseHold.Todos)
                .Distinct()
                .Include(t => t.Preferences)
                .Where(t => !t.IsCompleted)
                .ToListAsync();

            return householdTodos.Where(t => t.AssignedTo.Contains(userId))
                .ToList();
        }
    }
}