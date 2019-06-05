using System;
using System.Collections.Generic;

namespace Flexion.User.Infrastructure.DataModel
{
    public partial class UserRole
    {
        public UserRole()
        {
            User = new HashSet<User>();
        }

        public int UserRoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> User { get; set; }
    }
}
