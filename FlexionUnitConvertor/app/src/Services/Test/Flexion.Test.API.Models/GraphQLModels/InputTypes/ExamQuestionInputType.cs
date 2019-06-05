using Flexion.Test.API.Models.GraphQLModels.ModelTypes;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.InputTypes
{
    public class ExamQuestionInputType : InputObjectGraphType
    {
        public ExamQuestionInputType()
        {
            Name = "AddExamQuestionInput";
        
            Field<NonNullGraphType<IntGraphType>>("ExamID");
            Field<NonNullGraphType<IntGraphType>>("SourceConversionId");
            Field<NonNullGraphType<IntGraphType>>("DestinationConversionId");
            Field<NonNullGraphType<FloatGraphType>>("InputValue");
            






        }
    }
}

