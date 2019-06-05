using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Report.API.Models.GraphQLModels.ModelTypes
{
    public class ReportType : ObjectGraphType<Domain.DomainModel.Report>
    {
        public ReportType()
        {
            Field(x => x.ExamDate, true);
            Field(x => x.ExamDescription);
            Field(x => x.ExamId, true);
            Field(x => x.ExamQuestion.InputUnitOfMeasure, true);
            Field(x => x.ExamQuestion.InputValue, true);
            Field(x => x.ExamQuestion.IsCorrect, true);
            Field(x => x.ExamQuestion.OutPutUnitOfMeasure, true);
            Field(x => x.ExamQuestion.StudentID, true);
            Field(x => x.ExamQuestion.StudentName, true);
            Field(x => x.ExamQuestion.StudentResponse, true);
            Field(x => x.ExamQuestion.TeacherName);
            Field<ExamQuestionType>("ExamQuestion", resolve: context => context.Source.ExamQuestion);
            Field<ListGraphType<ExamQuestionType>>("ExamQuestions", resolve: context => context.Source.ExamQuestion);

        }
    }
}
