using Flexion.Test.API.Models.GraphQLModels.ModelTypes;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.InputTypes
{
    public class ExamAnswerInputType : InputObjectGraphType
    {
        public ExamAnswerInputType()
        {
            Name = "AddExamAnswerInput";
            Field<IntGraphType>("ExamQuestionId");
            Field<NonNullGraphType<FloatGraphType>>("Answer");
          


    }
}
}

