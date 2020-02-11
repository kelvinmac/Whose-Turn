using System;
using System.ComponentModel.DataAnnotations;

namespace Whose_Turn.Models.Account
{
    public class CreateUserModel
    {
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the users password
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,64}$", MatchTimeoutInMilliseconds = 100,
            ErrorMessage = "Password must containt Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string Password { get; set; }
    }
}
