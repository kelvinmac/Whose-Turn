using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Whose_Turn.Models.Household;

namespace Whose_Turn.Models.Todo
{
    /// <summary>
    /// Represents a todo's description
    /// </summary>
    public class TodoDescription
    {
        /// <summary>
        /// Gets or sets a list of house members assigned to the todo
        /// </summary>
        [Required, MinLength(1, ErrorMessage = "At least one household member is required"),
            JsonProperty("members")]
        public List<HouseholdMember> Members { get; set; }

        /// <summary>
        /// Gets or sets the todo's name
        /// </summary>
        [Required, JsonProperty("todoName")]
        public string TodoName { get; set; }

        /// <summary>
        /// Gets or sets the todo's start date
        /// </summary>
        [Required, JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the todo's end date
        /// </summary>
        [Required, JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
    }
}
