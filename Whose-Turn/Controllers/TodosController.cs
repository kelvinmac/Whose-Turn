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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whose_Turn.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TodosController : BaseController
    {
        private static class LogEvents
        {
            public static readonly EventId Patching = new EventId(1, nameof(Patch));

            public static readonly EventId Posting = new EventId(2, nameof(Post));
        }

        private readonly ILogger _logger;

        private readonly ITodoRepository _todoRepo;
        private readonly IHouseholdRepository _householdRepo;

        public TodosController(IServiceProvider provider)
        {
            _logger = provider.GetService<ILogger<TodosController>>();

            _householdRepo = provider.GetService<IHouseholdRepository>();
            _todoRepo = provider.GetService<ITodoRepository>();
        }

        // GET: api/To-dos
        [HttpGet]
        public IActionResult Get()
        {
            return RedirectToAction(nameof(Get), new { id = Guid.NewGuid().ToString() });
        }

        // GET api/To--dos/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(new List<Todo> {
            new Todo()
            {
                Id = Guid.NewGuid(),
                AssignedTo = new List<Guid> { Guid.NewGuid() },
                CreatedBy = Guid.NewGuid(),
                DueOn = DateTime.Now.AddDays(1),
                CreatedOn = DateTime.UtcNow,
                Task = "Todo test 1"
            }});
        }

        // POST api/To-dos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateTodoModel todo)
        {
            var t = new Todo()
            {
                Id = Guid.NewGuid(),
                Task = todo.Task,
                DueOn = todo.DueOn,
                AssignedTo = todo.AssignedTo,
                CreatedBy = UserId,
                CreatedOn = DateTime.UtcNow
            };

            var myHouseHold = await _householdRepo.UsersHouseHold(UserId);

            foreach (var user in todo.AssignedTo)
            {
                if (!await _householdRepo.IsInHouseHold(myHouseHold, user))
                {
                    _logger.LogWarning(LogEvents.Posting, "Todo cannot be assinged to user {assingedTo} by user {userId}, Not in household {houseHoldId}",
                        user, UserId, myHouseHold);

                    return new JsonHttpStatusResult(new ErrorModel()
                    {
                        Title = "Please validate the information entered",
                        Status = (int)HttpStatusCode.BadRequest,
                        TraceId = HttpContext.TraceIdentifier
                    },
                        HttpStatusCode.BadRequest);
                }
            }

            await _todoRepo.AddNewAsync(t);
            return Json(t);
        }

        // PUT api/To-dos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // PATCH api/To-dos
        [HttpPatch("{id}")]
        public JsonResult Patch(Guid id, [FromBody]Todo todoData)
        {
            
            try
            {
                todoData.Task = "lol";
            }
            catch (JsonSerializationException e)
            {
                return Json(new ErrorModel()
                {
                    Title = e.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                using (_logger.BeginScope(new Dictionary<string, string>
                {
                    ["jsonStr"] = JsonConvert.SerializeObject(todoData)
                }))
                    _logger.LogError(LogEvents.Patching, e, "An exception was thrown while deserializing json data");

                return new JsonHttpStatusResult(new ErrorModel()
                {
                    Title = e.Message,
                    Status = (int)HttpStatusCode.BadRequest,
                    TraceId = HttpContext.TraceIdentifier
                }, HttpStatusCode.InternalServerError);
            }

            return Json(null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
