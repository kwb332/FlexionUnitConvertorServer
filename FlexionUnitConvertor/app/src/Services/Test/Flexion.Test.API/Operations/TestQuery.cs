
using Flexion.Test.API.Models.GraphQLModels.InputTypes;
using Flexion.Test.API.Models.GraphQLModels.ModelTypes;
using Flexion.Test.Application.ApplicationInterface;
using Flexion.Test.Domain.DomainInterface;
using GraphQL.Types;
using System.Collections.Generic;

namespace Flexion.Test.API.Operations
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(ITestApplicationDriver testDriver)
        {
           
            Name = "TestQuery";

            #region Exam
            Field<ExamType>(
           "examByID",
           arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "examID" }),
           resolve: context => testDriver.GetExam(context.GetArgument<int>("examID")));
            Field<ListGraphType<ExamType>>(
         "examByUserID",
         arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userID" }),
         resolve: context => testDriver.GetExamByUser(context.GetArgument<int>("userID")));
            Field<ListGraphType<ExamType>>(
          "exams",
          resolve: context => testDriver.GetExams());
            Field<ListGraphType<ExamQuestionType>>(
       "examQuestions",
       arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "examID" }),
       resolve: context => testDriver.GetExamQuestions(context.GetArgument<int>("examID")));

            #endregion
            #region Conversion
            Field<ConversionType>(
           "conversionByID",
           arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "conversionID" }),
           resolve: context => testDriver.GetConversion(context.GetArgument<int>("conversionID")));
            Field<ListGraphType<ConversionType>>(
          "conversions",
          resolve: context => testDriver.GetConversionTable());

            #endregion

        }
    }
}