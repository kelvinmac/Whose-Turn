using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class UserRoles
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid IdentityRoleId { get; set; }

        public virtual Roles IdentityRole { get; set; }
        public virtual Users User { get; set; }
    }
}
