using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Whose_Turn.Models.Household;
using Whose_Turn.Services;

namespace Whose_Turn.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HouseholdController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IHouseholdRepository _householdRepo;

        public HouseholdController(IServiceProvider serviceProvider)
        {
            _householdRepo = serviceProvider.GetService<IHouseholdRepository>();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<HouseholdMember>> Mine()
        {
            var householdMemembers = await _householdRepo.UserHouseholdMembers(UserId);

            var houseMembers = new List<HouseholdMember>();
            householdMemembers.ForEach(u =>
            {
                houseMembers.Add(new HouseholdMember()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
            });

            return houseMembers;
        }
        // GET: api/Household
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Household/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Household
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Household/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
