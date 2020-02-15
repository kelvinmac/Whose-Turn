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
    public class HouseholdRepository : Repository<HouseHold, Guid>, IHouseholdRepository
    {
        /// <summary>
        /// Gets the local database context
        /// </summary>
        private DatabaseContext LocalContext { get; }

        public HouseholdRepository(DatabaseContext  context)
            :base(context)
        {
            LocalContext = context;
        }

        public async Task<bool> IsInHouseHold(Guid houseHoldId, Guid userId)
            => (await LocalContext.HouseHolds.FirstOrDefaultAsync(h => h.Id == houseHoldId))
                  .Users.Any(u => u.Id == userId);


        public Task<Guid> UsersHouseHold(Guid userId)
            => LocalContext.Users.Where(u => u.Id == userId)
            .Select(u => u.HouseHoldId)
            .FirstOrDefaultAsync();

        public Task<List<User>> HouseholdUsers(Guid householdId)
            => LocalContext.Users.Where(u => u.HouseHoldId == householdId)
            .ToListAsync();

        public Task<List<User>> UserHouseholdMembers(Guid userId)
            => LocalContext.Users.Where(u => u.Id == userId)
            .Include(u => u.MyHouseHold)
            .Select(u => u.MyHouseHold)
            .SelectMany(h => h.Users)
            .Distinct()
            .ToListAsync();
            
    }
}
