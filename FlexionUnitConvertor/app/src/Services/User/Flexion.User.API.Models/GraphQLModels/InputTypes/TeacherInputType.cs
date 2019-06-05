
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.API.Models.GraphQLModels.InputTypes
{
    public class TeacherInputType : InputObjectGraphType
    {
        public TeacherInputType()
        {
         
            Name = "AddTeacherInput";
            Field<StringGraphType>("FirstName");
            Field<NonNullGraphType<StringGraphType>>("LastName");
            Field<NonNullGraphType<IntGraphType>>("UserRoleID");



        }
    }
}

