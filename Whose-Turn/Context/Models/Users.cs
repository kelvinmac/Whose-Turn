using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class Users
    {
        public Users()
        {
            UserRoles = new HashSet<UserRoles>();
            UsersClaims = new HashSet<UsersClaims>();
        }

        public Guid Id { get; set; }
        public string VmeKeyHash { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime? AccountClosedTime { get; set; }
        public bool AccountClosed { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public Guid? MasterAccountId { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<UsersClaims> UsersClaims { get; set; }
    }
}
