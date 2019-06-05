using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.Domain.DomainModel
{
    public class UserRole
    {
      
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> User { get; set; }
    }
}
