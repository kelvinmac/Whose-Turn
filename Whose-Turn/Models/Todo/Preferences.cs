using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Whose_Turn.Context.Entities;

namespace Whose_Turn.Models.Todo
{
    /// <summary>
    /// Represents a todo's preferences
    /// </summary>
    public class Preferences
    {
        /// <summary>
        /// Gets or sets the todo's repeat
        /// </summary>
        [Required, JsonProperty("repeat")]
        public TodoRepeat Repeat { get; set; }

        /// <summary>
        /// Gets or sets the todo's priority
        /// </summary>
        [Required, JsonProperty("priority")]
        public TodoPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if other users are allowed to edit the todo
        /// </summary>
        public bool AllowEdits { get; set; }

        /// <summary>
        /// Gets or sets the creator subscribes to todo's updates
        /// </summary>
        public bool AcceptUpdates { get; set; }
    }
}