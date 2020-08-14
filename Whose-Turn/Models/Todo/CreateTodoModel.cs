using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Whose_Turn.Models.Todo
{
    public class CreateTodoModel
    {
        /// <summary>
        /// Gets or sets the todo Prov
        /// </summary>
        [Required]
        public TodoPrivacySettings Privacy { get; set; }

        /// <summary>
        /// Gets or sets the todo's description
        /// </summary>
        [Required]
        public TodoDescription Description { get; set; }

        /// <summary>
        /// Gets or sets the todo's details
        /// </summary>
        [Required]
        public TodoDetails Details { get; set; }

        /// <summary>
        /// Gets or sets the todo's preferences
        /// </summary>
        [Required] 
        public Preferences Preferences { get; set; }
    }
}
