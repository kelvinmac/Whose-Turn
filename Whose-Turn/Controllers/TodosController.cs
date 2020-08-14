using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Whose_Turn.Context.Entities;
using Whose_Turn.Models;
using System.Net;
using Whose_Turn.Services;
using Whose_Turn.Models.Todo;
using Microsoft.AspNetCore.Authorization;
using Whose_Turn.Controllers.Mixins;
using Whose_Turn.Models.Household;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whose_Turn.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TodosController : BaseController {
        private static class LogEvents {
            public static readonly EventId Patching = new EventId(1, nameof(Patch));

            public static readonly EventId Posting = new EventId(2, nameof(Post));

            public static readonly EventId Getting = new EventId(3, nameof(Get));
        }

        private readonly ILogger _logger;

        private readonly ITodoRepository _todoRepo;
        private readonly IHouseholdRepository _householdRepo;

        private readonly HouseholdMixins _householdMixins;
        private readonly TodosMixins _todosMixins;

        public TodosController(IServiceProvider provider) {
            _logger = provider.GetService<ILogger<TodosController>>();
            _householdMixins = provider.GetService<HouseholdMixins>();
            _todosMixins = provider.GetService<TodosMixins>();

            _householdRepo = provider.GetService<IHouseholdRepository>();
            _todoRepo = provider.GetService<ITodoRepository>();
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<IActionResult> All() {
            var userTodos = await _todoRepo.GetActiveUserTodosAsync(UserId);
            
            var models = this.MapMultipleTodoEntitiesToModels(userTodos);
            return Json(models);
        }

        // GET api/Todos/guid
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(Guid id) {
            var todo = await _todoRepo.GetByIdAsync(id);

            if (todo == null) {
                _logger.LogWarning(LogEvents.Getting, "Could not find a todo with the id {todoId}", id);
                return this.CreateTodoNotFoundError(id);
            }

            var model = this.MapTodoEntityToModel(todo);
            return Json(model);
        }

        // POST api/Todo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTodoModel model) {
            var todo = _todosMixins.CreateTodoFromModel(model, UserId);

            foreach (var member in model.Description.Members) {
                if (!await _householdMixins.IsUserInSameHouseHoldAsync(UserId, member.Id)) {
                    _logger.LogWarning(LogEvents.Posting,
                        "User {userId} and user {assignedUserId} not in the same household",
                        UserId, member.Id);

                    return this.CreateInvalidHouseholdError();
                }

                todo.AssignedTo.Add(member.Id);
            }

            await _todoRepo.AddNewAsync(todo);
            
            var result = this.MapTodoEntityToModel(todo);
            result.HouseholdMembers = this.MapUserIdToHouseholdMemberModels(todo.AssignedTo);
            
            _logger.LogInformation(LogEvents.Posting, "Successfully created Todo {todoId} for user {userId}",
                todo.Id, UserId);
            
            return Json(result);
        }

        // PUT api/Todos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // PATCH api/Todos
        [HttpPatch("{id}")]
        public JsonResult Patch(Guid id, [FromBody] Todo todoData) {
            try {
                todoData.TodoName = "lol";
            }
            catch (JsonSerializationException e) {
                return Json(new ErrorModel()
                {
                    Title = e.Message,
                    Status = (int) HttpStatusCode.InternalServerError,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e) {
                using (_logger.BeginScope(new Dictionary<string, string>
                {
                    ["jsonStr"] = JsonConvert.SerializeObject(todoData)
                }))
                    _logger.LogError(LogEvents.Patching, e, "An exception was thrown while deserializing json data");

                return new JsonHttpStatusResult(new ErrorModel()
                {
                    Title = e.Message,
                    Status = (int) HttpStatusCode.BadRequest,
                    TraceId = HttpContext.TraceIdentifier
                }, HttpStatusCode.InternalServerError);
            }

            return Json(null);
        }

        // DELETE api/Todos/guid
        [HttpDelete("{id}")]
        public void Delete(Guid id) { }
    }
}