using System;
using System.ComponentModel.DataAnnotations;

namespace Whose_Turn.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
