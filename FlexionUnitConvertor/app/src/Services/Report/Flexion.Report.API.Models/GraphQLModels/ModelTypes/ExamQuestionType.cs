using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Report.API.Models.GraphQLModels.ModelTypes
{
    public class ExamQuestionType : ObjectGraphType<Domain.DomainModel.ExamQuestion>
    {
        public ExamQuestionType()
        {
            Field(x => x.ExamId,true);
            Field(x => x.InputUnitOfMeasure);
            Field(x => x.InputValue,true);
            Field(x => x.IsCorrect,true);
            Field(x => x.OutPutUnitOfMeasure);
            Field(x => x.StudentID);
            Field(x => x.StudentName);
            Field(x => x.StudentResponse,true);
            Field(x => x.TeacherName);
        
        }
    }
}
