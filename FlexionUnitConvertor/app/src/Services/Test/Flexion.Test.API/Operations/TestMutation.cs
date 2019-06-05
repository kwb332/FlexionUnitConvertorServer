using Flexion.Test.API.Models.GraphQLModels.InputTypes;
using Flexion.Test.API.Models.GraphQLModels.ModelTypes;
using Flexion.Test.Application.ApplicationInterface;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Flexion.Test.API.Operations.EnumOperations;

namespace Flexion.Test.API.Operations
{
    public class TestMutation : ObjectGraphType
    {
        private readonly ITestApplicationDriver _testDriver;
        private readonly ILogger _logger;

        public TestMutation(ITestApplicationDriver testDriver, ILogger<TestMutation> logger)
        {
            _testDriver = testDriver;
            _logger = logger;


            Name = "TestMutation";

            Field<BooleanGraphType>(
                "AddExam",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ExamInputType>> { Name = "examAdd" }),
                resolve: context => ManageExams(context, ExamOperations.AddExam));

            Field<BooleanGraphType>(
                "AddQuestion",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ExamQuestionInputType>> { Name = "questionAdd" }),
                resolve: context => ManageExams(context, ExamOperations.AddQuestion));
            Field<BooleanGraphType>(
               "AddAnswer",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ExamAnswerInputType>> { Name = "answerAdd" }),
               resolve: context => ManageExams(context, ExamOperations.AddAnswer));
            Field<ListGraphType<ReportType>>(
              "submitToTeacher",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ExamInputType>> { Name = "submitTeacher" }),
              resolve: context => ManageExams(context, ExamOperations.SubmitExamToTeacher));
            Field<BooleanGraphType>(
              "submitToStudent",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ExamInputType>> { Name = "submitStudent" }),
              resolve: context => ManageExams(context, ExamOperations.SubmitExamToStudent));

        }

        public async Task<object> ManageExams(ResolveFieldContext<object> context, ExamOperations operation)
        {
            try
            {
                object exam = null;

                switch (operation)
                {
                    case ExamOperations.AddExam:
                        var addExam = context.GetArgument<Domain.DomainModel.Exam>("examAdd");
                        exam = await _testDriver.CreateExam(addExam);
                        break;
                    case ExamOperations.AddQuestion:
                        var questionAdd = context.GetArgument<Domain.DomainModel.ExamQuestion>("questionAdd");
                        exam = await _testDriver.AddQuestion(questionAdd);
                        break;
                    case ExamOperations.AddAnswer:
                        var answerAdd = context.GetArgument<Domain.DomainModel.ExamQuestionAnswer>("answerAdd");
                        exam = await _testDriver.AddAnswer(answerAdd);
                        break;
                    case ExamOperations.SubmitExamToTeacher:
                        var submitTeacher = context.GetArgument<Domain.DomainModel.Exam>("submitTeacher");
                        exam = await _testDriver.SubmitExamToTeacher(submitTeacher);
                        break;
                    case ExamOperations.SubmitExamToStudent:
                        var submitStudent = context.GetArgument<Domain.DomainModel.Exam>("submitStudent");
                        exam = await _testDriver.SubmitExamToStudent(submitStudent);
                        break;


                }

                return exam;
            }
            catch (ExecutionError ex)
            {
                context.Errors.Add(new ExecutionError(ex.Message));
            }
            catch (AggregateException ex)
            {
                context.Errors.Add(new ExecutionError(ex.Message));
            }
            catch (Exception ex)
            {
                context.Errors.Add(new ExecutionError(ex.Message));
            }

            return null;
        }

      
    }
}