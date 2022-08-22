using System;
using System.Collections.Generic;

namespace StudActive.Entities
{
    public partial class UserRole
    {
        public Guid UserRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
