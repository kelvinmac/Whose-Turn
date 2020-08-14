using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Whose_Turn.Context.Entities
{
    /// <summary>
    /// Todo repetition type
    /// </summary>
    public enum TodoRepeat
    {
        None = 1,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    /// <summary>
    /// Different priorities that can be used by a todo
    /// </summary>
    public enum TodoPriority
    {
        Normal = 1,
        Medium,
        High
    }

    /// <summary>
    /// The todo privacy settings
    /// </summary>
    public enum TodoPrivacy
    {
        Private = 1,
        Household
    }

    /// <summary>
    /// Entity representing a to-do task
    /// </summary>
    public class Todo
    {
        public Todo()
        {
            AssignedTo = new HashSet<Guid>();
        }
        
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
        /// Gets or sets the date the task was completed
        /// </summary>
        public DateTime? CompletedOn { get; set; }

        /// <summary>
        /// Gets or sets th todo's preferences
        /// </summary>
        public TodoPreferences Preferences { get; set; }

        /// <summary>
        /// Gets or sets the preferences identifier 
        /// </summary>
        public Guid PreferencesId { get; set; }
        
        /// <summary>
        /// Gets or sets the the HouseHold id
        /// </summary>
        public Guid HouseHoldId { get; set; }
        
        /// <summary>
        /// Gets or sets the user's household
        /// </summary>
        public HouseHold UsersHouseHold { get; set; }

        /// <summary>
        /// Gets or sets the Ids of user assigned to the Todo
        /// </summary>
        public ICollection<Guid> AssignedTo { get; set; }
    }
}
