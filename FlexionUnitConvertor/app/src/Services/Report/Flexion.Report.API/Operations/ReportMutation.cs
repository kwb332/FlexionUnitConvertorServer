using Flexion.Report.API.Models.GraphQLModels.InputTypes;
using Flexion.Report.Application.ApplicationInterface;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Flexion.Report.API.Operations.EnumOperations;

namespace Flexion.Report.API.Operations
{
    public class ReportMutation : ObjectGraphType
    {
        private readonly IReportApplicationDriver _reportDriver;
        private readonly ILogger _logger;

        public ReportMutation(IReportApplicationDriver reportDriver, ILogger<ReportMutation> logger)
        {
            _reportDriver = reportDriver;
            _logger = logger;


            Name = "ReportMutation";

            Field<BooleanGraphType>(
                "AddReport",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ReportInputType>> { Name = "reportAdd" }),
                resolve: context => ManageReport(context, ReportOperations.AddReport));

            Field<BooleanGraphType>(
               "AddReports",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ListGraphType<ReportInputType>>> { Name = "reportAdd" }),
               resolve: context => ManageReport(context, ReportOperations.AddReports));

        }

        public async Task<object> ManageReport(ResolveFieldContext<object> context, ReportOperations operation)
        {
            try
            {
                object report = null;

                switch (operation)
                {
                    case ReportOperations.AddReport:
                        var reportAdd = context.GetArgument<Domain.DomainModel.Report>("reportAdd");
                        report = await _reportDriver.AddReport(reportAdd);
                        break;
                    case ReportOperations.AddReports:
                        var reportAdds = context.GetArgument<List<Domain.DomainModel.Report>>("reportAdd");
                        report = await _reportDriver.AddReports(reportAdds);
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