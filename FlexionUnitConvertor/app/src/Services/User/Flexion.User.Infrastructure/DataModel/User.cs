using System;
using System.Collections.Generic;

namespace Flexion.User.Infrastructure.DataModel
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserRoleId { get; set; }

        public UserRole UserRole { get; set; }
    }
}
