using System;
using Microsoft.AspNetCore.Mvc;
using Whose_Turn.Context.Entities;
using Whose_Turn.Models.Todo;

namespace Whose_Turn.Controllers.Mixins
{
    /// <summary>
    /// Contains a series of methods for 
    /// </summary>
    public class TodosMixins
    {
        /// <summary>
        /// Creates a new <see cref="Todo"/> from the <see cref="CreateTodoFromModel"/>
        /// </summary>
        /// <param name="model"> The model instance </param>
        /// <param name="userId"> The <see cref="User"/> th model is being created for </param>
        /// <returns> The Todo instance </returns>
        public Todo CreateTodoFromModel(CreateTodoModel model, Guid userId)
        {
            var todo = new Todo()
            {
                Id = Guid.NewGuid(),
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                DueOn = model.Description.EndDate,
                TodoName = model.Description.TodoName,
                TodoDescription = model.Details.DetailedDescription,
                Preferences = new TodoPreferences()
                {
                    Id = Guid.NewGuid(),
                    AllowEdits = model.Preferences.AllowEdits,
                    EnableNotification = model.Preferences.AcceptUpdates,
                    Priority = model.Preferences.Priority,
                    Privacy = model.Privacy.Policy,
                    Repeat = model.Preferences.Repeat
                }
            };
            return todo;
        }
    }
}