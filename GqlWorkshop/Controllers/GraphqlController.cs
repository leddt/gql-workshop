using System.Collections;
using System.IO;
using System.Threading.Tasks;
using GraphQL.Conventions;
using Microsoft.AspNetCore.Mvc;

namespace GqlWorkshop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GraphqlController : ControllerBase
    {
        private readonly GraphQLEngine engine;
        private readonly IDependencyInjector injector;

        public GraphqlController(GraphQLEngine engine, IDependencyInjector injector)
        {
            this.engine = engine;
            this.injector = injector;
        }

        public async Task<IActionResult> Post()
        {
            string requestBody;
            using (var reader = new StreamReader(Request.Body))
                requestBody = await reader.ReadToEndAsync();

            var result = await engine
                .NewExecutor()
                .WithDependencyInjector(injector)
                .WithRequest(requestBody)
                .Execute();

            return new ContentResult {
                Content = engine.SerializeResult(result),
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}