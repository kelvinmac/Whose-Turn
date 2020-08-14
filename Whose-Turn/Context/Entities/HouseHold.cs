using System;
using System.Collections;
using System.Collections.Generic;

namespace Whose_Turn.Context.Entities
{
    /// <summary>
    /// Entity representing a household
    /// </summary>
    public class HouseHold
    {
        public HouseHold()
        {
            Users = new HashSet<User>();
            Todos = new HashSet<Todo>();
        }

        /// <summary>
        /// Gets or sets the household id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the household name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date the household was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the id of the user that created the household 
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the user incharge of this house
        /// </summary>
        public Guid ManOfTheHouse { get; set; }

        /// <summary>
        /// Gets or sets the the users in this household
        /// </summary>
        public ICollection<User> Users { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of todos for this household
        /// </summary>
        public ICollection<Todo> Todos { get; set; }
    }
}