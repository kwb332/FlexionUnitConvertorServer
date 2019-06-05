using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.ModelTypes
{
    public class ExamQuestionType : ObjectGraphType<Domain.DomainModel.ExamQuestion>
    {
        public ExamQuestionType()
        {
            Field(x => x.Answer.Answer, true);
            Field(x => x.ExamId);
            Field(x => x.ExamQuestionId);
            Field(x => x.Exam.Description, true);
            Field("DestinationConversionID",x => x.DestinationConversion.ConversionId,true);
            Field("DestinationConversionName",x => x.DestinationConversion.ConversionName,true);
            Field("SourceConversionID", x => x.SourceConversion.ConversionId, true);
            Field("SourceConversionName", x => x.SourceConversion.ConversionName, true);
            Field(x => x.InputValue,true);


        }
    }
}
