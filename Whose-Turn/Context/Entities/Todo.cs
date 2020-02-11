using System;
using System.Collections.Generic;

namespace Whose_Turn.Context.Entities
{
    /// <summary>
    /// Entity representing a to-do task
    /// </summary>
    public class Todo
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
        public string Task { get; set; }

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
        /// Gets or sets the date the task was completed
        /// </summary>
        public List<Guid> AssignedTo { get; set; }
    }
}
