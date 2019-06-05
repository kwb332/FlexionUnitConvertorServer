using Flexion.User.API.Models.GraphQLModels.ModelTypes;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.User.API.Models.GraphQLModels.InputTypes
{
    public class StudentInputType : InputObjectGraphType
    {
        public StudentInputType()
        {
            Name = "AddStudentInput";
            Field<StringGraphType>("FirstName");
            Field<NonNullGraphType<StringGraphType>>("LastName");
            Field<NonNullGraphType<IntGraphType>>("UserRoleID");
          
        }
    }
}

