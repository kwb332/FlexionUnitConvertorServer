using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.API.Models.GraphQLModels.ModelTypes
{
    public class UserRoleType : ObjectGraphType<Domain.DomainModel.UserRole>
    {
        public UserRoleType()
        {
            Field(x => x.RoleName,true);
            Field(x => x.UserRoleId);
         
        
        }
    }
}
