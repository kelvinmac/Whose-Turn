using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Whose_Turn.Context;
using Whose_Turn.Context.Entities;
using Whose_Turn.Services;

namespace Whose_Turn.Repositories
{
    public class HouseHoldRepository : Repository<HouseHold, Guid>, IHouseHoldRepository
    {
        /// <summary>
        /// Gets the local database context
        /// </summary>
        private DatabaseContext LocalContext { get; }

        public HouseHoldRepository(DatabaseContext  context)
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
    }
}
