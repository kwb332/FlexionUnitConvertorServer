
using Flexion.Report.API.Models.GraphQLModels.InputTypes;
using Flexion.Report.API.Models.GraphQLModels.ModelTypes;
using Flexion.Report.Application.ApplicationInterface;
using Flexion.Report.Domain.DomainInterface;
using GraphQL.Types;
using System.Collections.Generic;

namespace Flexion.Report.API.Operations
{
    public class ReportQuery : ObjectGraphType
    {
        public ReportQuery(IReportApplicationDriver reportDriver)
        {
           
            Name = "ReportQuery";

            #region Report
            Field<ListGraphType<ReportType>>(
           "reportByExamID",
           arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "examID" }),
           resolve: context => reportDriver.GetReportByID(context.GetArgument<int>("examID")));

            Field<ListGraphType<ReportType>>(
         "reportByUserID",
         arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userID" },
         new QueryArgument<IntGraphType> { Name = "examID" }),
         resolve: context => reportDriver.GetReportByUserID(context.GetArgument<int>("userID"), context.GetArgument<int>("examID")));

        

            #endregion

        }
    }
}