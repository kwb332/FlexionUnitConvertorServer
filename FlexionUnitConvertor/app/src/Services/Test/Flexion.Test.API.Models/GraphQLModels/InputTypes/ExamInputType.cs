using Flexion.Test.API.Models.GraphQLModels.ModelTypes;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.InputTypes
{
    public class ExamInputType : InputObjectGraphType
    {
        public ExamInputType()
        {

            Name = "ExamInput";
            Field<IntGraphType>("ExamId");
            Field<StringGraphType>("Description");
            Field<IntGraphType>("StudentId");
            Field<BooleanGraphType>("IsComplete");
            Field<BooleanGraphType>("IsGraded");
            Field<DateTimeGraphType>("DateCreated");
            Field<IntGraphType>("TeacherId");
            Field<DateGraphType>("ExamDate");
            Field<BooleanGraphType>("IsCreated");

          

  

    }
    }
}

