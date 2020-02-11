using System;
using System.Collections.Generic;

namespace Whose_Turn.Models.Todo
{
    public class CreateTodoModel
    {
        /// <summary>
        /// Gets or sets the due date
        /// </summary>
        public DateTime DueOn { get; set; }

        /// <summary>
        /// Gets or sets the to-do task
        /// </summary>
        public string Task { get; set; }

        /// <summary>
        /// Gets or sets a list of users the task is assinged to
        /// </summary>
        public List<Guid> AssignedTo { get; set; }
    }
}
