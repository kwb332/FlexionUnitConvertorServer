using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GraphQL.Validation;
using System.Collections.Generic;
using Flexion.User.API.Helpers;

namespace Flexion.User.API.Controllers
{
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly ILogger _logger;
        private readonly IEnumerable<IValidationRule> _validationRule;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter, 
            ILogger<GraphQLController> logger)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _logger = logger;
          
        }


        [HttpPost]
        [ServiceFilter(typeof(ResourceFilter))]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {

            if (query == null) {
                _logger.LogError("Argument null exception for query - CHECK {0}", nameof(query));
                throw new ArgumentNullException(nameof(query));
            }
            var inputs = query.Variables.ToInputs();
            try
            {

                var files = this.Request.HasFormContentType ? this.Request.Form.Files : null;
                var executionOptions = new ExecutionOptions
                {
                    Schema = _schema,
                    Query = query.Query,
                    Inputs = inputs,
                    Root = files,
                    ValidationRules=_validationRule
                };

                var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Something really went wrong - CHECK: {0}",ex.Message);
                return StatusCode(500,"Internal server error");
            }
        }

    }
    public class ResourceFilter : IResourceFilter
    {
        private readonly string jsonMediaType = "application/json";
        private readonly ILogger _logger;
        public ResourceFilter(ILogger<ResourceFilter> logger)
        {
            _logger = logger;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var req = context.HttpContext.Request;
            // Allows using several time the stream in ASP.Net Core
            req.EnableRewind();
            string reqBody = string.Empty;
            if (req.ContentType.Split(';')[0] == "multipart/form-data")
            {
                try
                {
                    var encoder = JavaScriptEncoder.Create();
                    
                    if (req?.Form.ContainsKey("query") ?? false)
                    {
                        reqBody = $"{{\"query\":\"{encoder.Encode(req.Form["query"])}\"}}";
                    }

                    byte[] requestData = Encoding.UTF8.GetBytes(reqBody);
                    req.Body = new MemoryStream(requestData);
                    req.ContentType = this.jsonMediaType;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,"Resource filter Exception- CHECK");
                }
            }
        }
    }
}