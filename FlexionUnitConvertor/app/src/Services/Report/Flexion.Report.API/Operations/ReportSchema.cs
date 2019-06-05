using GraphQL;
using GraphQL.Types;

namespace Flexion.Report.API.Operations
{
    public class ReportSchema : Schema
    {
        public ReportSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ReportQuery>();
            Mutation = resolver.Resolve<ReportMutation>();
        }
    }
}

