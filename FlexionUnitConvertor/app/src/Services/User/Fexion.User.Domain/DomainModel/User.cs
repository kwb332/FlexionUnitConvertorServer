using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.Domain.DomainModel
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
