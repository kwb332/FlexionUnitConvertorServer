
using Flexion.User.API.Models.GraphQLModels.InputTypes;
using Flexion.User.Application.ApplicationInterface;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Flexion.User.API.Operations.EnumOperations;

namespace Flexion.User.API.Operations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserApplicationDriver _userDriver;
        private readonly ILogger _logger;

        public UserMutation(IUserApplicationDriver userDriver, ILogger<UserMutation> logger)
        {
            _userDriver = userDriver;
            _logger = logger;


            Name = "UserMutation";

            Field<BooleanGraphType>(
                "AddStudent",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StudentInputType>> { Name = "studentAdd" }),
                resolve: context => ManageUser(context, UserOperations.AddStudent));
            Field<BooleanGraphType>(
               "AddTeacher",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TeacherInputType>> { Name = "teacherAdd" }),
               resolve: context => ManageUser(context, UserOperations.AddTeacher));

        }

        public async Task<object> ManageUser(ResolveFieldContext<object> context, UserOperations operation)
        {
            try
            {
                object report = null;

                switch (operation)
                {
                    case UserOperations.AddStudent:
                        var userAdd = context.GetArgument<Domain.DomainModel.Student>("studentAdd");
                        report = await _userDriver.AddStudent(userAdd);
                        break;
                    case UserOperations.AddTeacher:
                        var teacherAdd = context.GetArgument<Domain.DomainModel.Teacher>("teacherAdd");
                        report = await _userDriver.AddTeacher(teacherAdd);
                        break;


                }

                return report;
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