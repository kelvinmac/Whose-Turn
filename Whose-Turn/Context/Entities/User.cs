using System;
namespace Whose_Turn.Context.Entities
{
    /// <summary>
    /// Entity representing a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the Id
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
        /// Gets or sets the users email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password hash
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the security token 
        /// </summary>
        public string SecurityToken { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if this user requires two factor authentication
        /// </summary>
        public bool TwoFactorRequired { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the account has been closed
        /// </summary>
        public bool AccountClosed { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the account has been locked out
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Gets or sets the lockout reason
        /// </summary>
        public string LockoutReason { get; set; }

        /// <summary>
        /// Gets or sets lockout end date
        /// </summary>
        public DateTime? LockoutEndDate { get; set; }

        /// <summary>
        /// Gets or sets the last successful login
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the household id
        /// </summary>
        public Guid HouseHoldId { get; set; }

        /// <summary>
        /// Gets or sets the users household
        /// </summary>
        public HouseHold MyHouseHold { get; set; }

        /// <summary>
        /// Gets or sets the date the user was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
