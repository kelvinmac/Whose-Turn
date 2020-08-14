using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Whose_Turn.Context.Entities;
using Whose_Turn.Models.Todo;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Whose_Turn.Managers;
using Whose_Turn.Models;
using Whose_Turn.Models.Household;
using Whose_Turn.Services;

namespace Whose_Turn.Controllers.Mixins {
    public static class ControllerExtensions {
        private static class LogEvents {
            public static readonly EventId TodoNotFound = new EventId(2, nameof(CreateTodoNotFoundError));
        }

        /// <summary>
        /// Maps the a <see cref="Todo"/> entity to the <see cref="TodoModel"/> instance. Does not initialise AssignedTo
        /// </summary>
        /// <param name="controller"> Controller instance </param>
        /// <param name="entity"> The entity instance </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the entity or entity.preferences is null</exception>
        public static TodoModel MapTodoEntityToModel(this Controller controller, Todo entity) {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Preferences == null)
                throw new ArgumentNullException(nameof(entity.Preferences));

            var todoModelMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Todo, TodoModel>()).CreateMapper();

            var preferencesMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<TodoPreferences, Preferences>()).CreateMapper();

            var model = todoModelMapper.Map<TodoModel>(entity);
            model.TodoPreferences = preferencesMapper.Map<Preferences>(entity.Preferences);

            return model;
        }

        /// <summary>
        ///  Maps the multiple<see cref="Todo"/> entities to the <see cref="TodoModel"/> instance. Does not initialise AssignedTo
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="entities"> The entity collection</param>
        /// <returns> A list of todo models</returns>
        public static List<TodoModel> MapMultipleTodoEntitiesToModels(this Controller controller,
            ICollection<Todo> entities) {
            return entities.Select(entity => MapTodoEntityToModel(controller, entity)).ToList();
        }

        /// <summary>
        /// Maps a list of User IDs to <see cref="HouseholdMember"/> instances
        /// </summary>
        /// <param name="controller"> Controller instance </param>
        /// <param name="userIds"> A list of user id</param>
        /// <returns></returns>
        public static List<HouseholdMember> MapUserIdToHouseholdMemberModels(this Controller controller, ICollection<Guid> userIds) {
            var memberInstances = new List<HouseholdMember>();

            if (!userIds.Any())
                return memberInstances;

            var userManager = controller.HttpContext.RequestServices.GetService<WhoseTurnUserManager>();

            memberInstances.AddRange(from userId in userIds
                let user = userManager.FindUserById(userId)
                select new HouseholdMember() {Id = userId, FirstName = user.FirstName, LastName = user.LastName,});

            return memberInstances;
        }

        /// <summary>
        /// Creates a list of todo modes from the given <see cref="Todo"/> identifiers
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="todoIds"> List of todos</param>
        /// <returns></returns>
        public static async Task<List<TodoModel>> CreateTodoModelsFromIdsAsync(this Controller controller,
            List<Guid> todoIds) {
            if (todoIds == null || !todoIds.Any())
                throw new ArgumentNullException(nameof(todoIds));

            var todoRepo = controller.HttpContext.RequestServices.GetService<ITodoRepository>();
            var todos = await RetrieveTodosFromIdsAsync(todoRepo, todoIds);

            var models = new List<TodoModel>();

            todos.ForEach(todo => { models.Add(controller.MapTodoEntityToModel(todo)); });

            return models;
        }


        /// <summary>
        /// Retrieves all the todos with the given ids
        /// </summary>
        /// <param name="repo"> Repository </param>
        /// <param name="todoIds"> The list of todo identifier </param>
        /// <returns> Collection of Todos</returns>
        private static async Task<List<Todo>> RetrieveTodosFromIdsAsync(ITodoRepository repo, List<Guid> todoIds) {
            var todos = new List<Todo>();

            foreach (var todoId in todoIds)
                todos.Add(await repo.GetByIdAsync(todoId));

            return todos;
        }

        /// <summary>
        /// Creates and returns a JsonResult for NotFound Todo
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="todoId"></param>
        /// <returns></returns>
        public static JsonResult CreateTodoNotFoundError(this Controller controller, Guid todoId) {
            return new JsonHttpStatusResult(new ErrorModel()
            {
                Status = (int) HttpStatusCode.BadGateway,
                Title = "Could not find or more of the Todos",
                TraceId = controller.HttpContext.TraceIdentifier
            }, HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Creates an invalid house hold error result
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public static JsonResult CreateInvalidHouseholdError(this Controller controller) {
            return new JsonHttpStatusResult(new ErrorModel()
            {
                Status = (int) HttpStatusCode.BadGateway,
                Title = "User household mismatch, please try again",
                TraceId = controller.HttpContext.TraceIdentifier
            }, HttpStatusCode.BadRequest);
        }
    }
}