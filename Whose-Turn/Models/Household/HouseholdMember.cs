using System;
namespace Whose_Turn.Models.Household
{
    public class HouseholdMember
    {
        /// <summary>
        /// Gets or sets the user id 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public string LastName { get; set; }
    }
}
