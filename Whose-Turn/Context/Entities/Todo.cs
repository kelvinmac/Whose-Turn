using System;
namespace Whose_Turn.Context.Entities
{
    public class Todo
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
