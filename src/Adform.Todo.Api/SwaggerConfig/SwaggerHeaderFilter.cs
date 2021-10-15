
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Adform.Todo.Api.SwaggerSupport
{
    /// <summary>
    /// Swagger custom filter to get header values
    /// </summary>
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "HEADER",
                In = ParameterLocation.Header,
                Style = ParameterStyle.DeepObject,
                Required = false
            }); ;
        }
    }
}
