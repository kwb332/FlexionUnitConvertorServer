
using Flexion.User.API.Models.GraphQLModels.ModelTypes;
using Flexion.User.Application.ApplicationInterface;
using Flexion.User.Domain.DomainInterface;
using GraphQL.Types;
using System.Collections.Generic;

namespace Flexion.User.API.Operations
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserApplicationDriver userDriver)
        {
           
            Name = "UserQuery";

            #region Report
            Field<StudentType>(
           "studentByID",
           arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userID" }),
           resolve: context => userDriver.GetStudentByID(context.GetArgument<int>("userID")));
            Field<TeacherType>(
          "teacherByID",
          arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userID" }),
          resolve: context => userDriver.GetTeacherByID(context.GetArgument<int>("userID")));
            Field<ListGraphType<StudentType>>(
          "students",
       
          resolve: context => userDriver.GetStudents());
            Field<ListGraphType<TeacherType>>(
         "teachers",
         resolve: context => userDriver.GetTeachers());



            #endregion

        }
    }
}