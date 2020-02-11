using System;
using System.Threading.Tasks;
using Whose_Turn.Context.Entities;
using Whose_Turn.Repositories;

namespace Whose_Turn.Services
{
    public interface IHouseHoldRepository : IRepository<HouseHold, Guid>
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
        Task<Guid> UsersHouseHold(Guid userId);
    }
}
