using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Whose_Turn.Context.Entities;
using Whose_Turn.Services;

namespace Whose_Turn.Controllers.Mixins
{
    public class HouseholdMixins
    {
        private readonly IHouseholdRepository _householdRepo;
        
        public HouseholdMixins(IServiceProvider serviceProvider)
        {
            _householdRepo = serviceProvider.GetService<IHouseholdRepository>();
        }
        
        /// <summary>
        /// Checks if the two users are in the same household
        /// </summary>
        /// <param name="userA"> The first User </param>
        /// <param name="userB"> The second User </param>
        /// <returns></returns>
        public async Task<bool> IsUserInSameHouseHoldAsync(Guid userA, Guid userB)
        {
            var houseHoldId = await _householdRepo.GetUserHouseHoldId(userA);

            return await _householdRepo.IsInHouseHold(houseHoldId, houseHoldId);
        }
    }
}