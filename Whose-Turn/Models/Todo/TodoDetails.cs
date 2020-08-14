using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Whose_Turn.Models.Todo
{
    /// <summary>
    /// A class representing the todo's details
    /// </summary>
    public class TodoDetails
    {
        /// <summary>
        /// Gets or sets the todo's detailed description
        /// </summary>
        [Required, JsonProperty("detailedDescription")]
        public string DetailedDescription { get; set; }
    }
}
