using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Whose_Turn.Context.Entities;

namespace Whose_Turn.Models.Todo
{
    /// <summary>
    /// Class representing a todo's privacy settings
    /// </summary>
    public class TodoPrivacySettings
    {
        /// <summary>
        /// Gets or sets the todo's privacy
        /// </summary>
        [Required, JsonProperty("policy")]

        public TodoPrivacy Policy { get; set; }
    }
}
