using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.API.Models.GraphQLModels.ModelTypes
{
    public class StudentType : ObjectGraphType<Domain.DomainModel.Student>
    {
        public StudentType()
        {
            Field(x=>x.FirstName);
            Field(x => x.LastName);
            Field(x => x.UserId);
            Field(x => x.UserRole.RoleName);
            Field(x => x.UserRole.UserRoleId);
           
        }
    }
}
