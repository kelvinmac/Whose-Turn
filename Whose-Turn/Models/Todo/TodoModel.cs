using System;
using System.Collections.Generic;
using Whose_Turn.Models.Household;

namespace Whose_Turn.Models.Todo
{
    /// <summary>
    /// Represents a model returned for a Todo entity request
    /// </summary>
    public class TodoModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date the to-do was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the due date
        /// </summary>
        public DateTime DueOn { get; set; }

        /// <summary>
        /// Gets or sets the task of the to-do
        /// </summary>
        public string TodoName { get; set; }
        
        /// <summary>
        /// Gets or sets the todo's description 
        /// </summary>
        public string TodoDescription { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating if the task has been completed
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the user id that completed the task
        /// </summary>
        public Guid? CompletedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the todo's preferences
        /// </summary>
        public Preferences TodoPreferences { get; set; }
        
        /// <summary>
        /// Gets or sets the date the task was completed
        /// </summary>
        public List<HouseholdMember> HouseholdMembers { get; set; }
    }
}