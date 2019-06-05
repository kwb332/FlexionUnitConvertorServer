using GraphQL;
using GraphQL.Types;

namespace Flexion.Test.API.Operations
{
    public class TestSchema : Schema
    {
        public TestSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TestQuery>();
            Mutation = resolver.Resolve<TestMutation>();
        }
    }
}

