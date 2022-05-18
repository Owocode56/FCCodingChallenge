using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FCCodingChallenge.API.Data
{
    public class RequiredHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "ApiKey",
                In = ParameterLocation.Header,
                Required = false,
                Example = new OpenApiString("xxxxxxx:xxxxxx:Xxxxxx:xxxxxx")
            });
        }
    }
}
