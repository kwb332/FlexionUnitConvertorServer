using Flexion.Test.Domain.DomainModel;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.ModelTypes
{
    public class ReportType : ObjectGraphType<Report>
    {
        public ReportType()
        {
            Field(x => x.ExamDate, true);
            Field(x => x.ExamDescription);
            Field(x => x.ExamId, true);
            Field(x => x.InputUnitOfMeasure, true);
            Field(x => x.OutPutUnitOfMeasure, true);
            Field(x => x.ExamQuestion.Exam.StudentId, true);
            Field(x => x.StudentName, true);
            Field(x => x.InputValue, true);
            Field(x => x.IsCorrect, true);
            Field(x => x.StudentID);
            Field(x => x.StudentResponse, true);
            Field(x => x.TeacherName);
            Field(x => x.TeacherID, true);
            Field<ExamQuestionType>("ExamQuestion", resolve: context => context.Source.ExamQuestion);
            Field<ListGraphType<ExamQuestionType>>("ExamQuestions", resolve: context => context.Source.ExamQuestion);

        }
    }
}
    
 

