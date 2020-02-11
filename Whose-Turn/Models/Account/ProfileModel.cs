using System;
namespace Whose_Turn.Models.Account
{
    public class ProfileModel
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

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the household id
        /// </summary>
        public string HouseHoldId { get; set; }

        /// <summary>
        /// Gets or sets the security token
        /// </summary>
        public string SecurityToken { get; set; }
    }
}
