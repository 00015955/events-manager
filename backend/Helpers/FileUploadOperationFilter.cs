using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace backend.Helpers // Replace with your actual namespace
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFile[]));

            if (fileParams.Any())
            {

                operation.Parameters.Clear();
                
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = GenerateSchema(context)
                        }
                    }
                };
            }
        }

        private OpenApiSchema GenerateSchema(OperationFilterContext context)
        {

            var properties = new Dictionary<string, OpenApiSchema>();

            foreach (var parameter in context.MethodInfo.GetParameters())
            {
                var name = parameter.Name;

                if (parameter.ParameterType == typeof(IFormFile))
                {
                    properties[name] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    };
                }
                else
                {
                    // For other parameters, infer the schema
                    var schema = context.SchemaGenerator.GenerateSchema(parameter.ParameterType, context.SchemaRepository);
                    properties[name] = schema;
                }
            }

            return new OpenApiSchema
            {
                Type = "object",
                Properties = properties,
                Required = properties.Keys.ToList() as ISet<string>
            };
        }
    }
}
