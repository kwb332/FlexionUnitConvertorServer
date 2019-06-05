using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Flexion.User.API.Helpers
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; } //https://github.com/graphql-dotnet/graphql-dotnet/issues/389    
        //public object File { get; set; }
    }
}