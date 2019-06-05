using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.API.Models.GraphQLModels.ModelTypes
{
    public class TeacherType : ObjectGraphType<Domain.DomainModel.Teacher>
    {
       
            public TeacherType()
            {
                Field(x => x.FirstName);
                Field(x => x.LastName);
                Field(x => x.UserId);
                Field(x => x.UserRole.RoleName);
                Field(x => x.UserRole.UserRoleId);

            }

    }
}
