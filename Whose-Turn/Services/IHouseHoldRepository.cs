using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whose_Turn.Context.Entities;
using Whose_Turn.Repositories;

namespace Whose_Turn.Services
{
    public interface IHouseholdRepository : IRepository<HouseHold, Guid>
    {
        /// <summary>
        /// Returns true if the specified user is in the specified household
        /// </summary>
        /// <param name="houseHoldId"> The <see cref="HouseHold"/> identifier </param>
        /// <param name="userId"> The <see cref="User"/> identifier </param>
        /// <returns></returns>
        Task<bool> IsInHouseHold(Guid houseHoldId, Guid userId);

        /// <summary>
        /// Retrieves the household Id for the user the given id
        /// </summary>
        /// <param name="userId"> The <see cref="User"/> identifier</param>
        /// <returns></returns>
        Task<Guid> GetUserHouseHoldId(Guid userId);

        /// <summary>
        /// Retrieves the users in the household with the given id
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns></returns>
        Task<List<User>> HouseholdUsers(Guid householdId);

        /// <summary>
        /// Retrievs the users in the same household as the user provided
        /// </summary>
        /// <param name="userId"> The <see cref="User"/> identifier</param>
        /// <returns></returns>
        Task<List<User>> UserHouseholdMembers(Guid userId);
    }
}
