using System;
namespace Whose_Turn.Context.Entities
{
    /// <summary>
    /// Preferences base class
    /// </summary>
    public class TodoPreferences
    {
        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the todo
        /// </summary>
        public Todo Todo { get; set; }

        /// <summary>
        /// Gets or sets the todo's repeat
        /// </summary>
        public TodoRepeat Repeat { get; set; }

        /// <summary>
        /// Gets or sets the todo's priority
        /// </summary>
        public TodoPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the todo's privacy
        /// </summary>
        public TodoPrivacy Privacy { get; set; }

        /// <summary>
        /// Gets or sets a boolean whether creator gets notifications
        /// </summary>
        public bool EnableNotification { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if others are allowed to edit
        /// </summary>
        public bool AllowEdits { get; set; }
    }
}
