using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace KlzApi
{
    public class SwaggerOperationFilter : Swashbuckle.Swagger.IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.parameters.Add(new Swashbuckle.Swagger.Parameter {
                name = "auth",
                @in = "header",
                description = "默认token",
                required = false,
                type = "string",
                @default = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqd3RkYXRhIjowLCJJZCI6IjMyMGUwNzUzLTFlZTItNGY0Yi04OTE5LTRjYjIzNzAzMDMzNCIsIkNyZWF0ZURhdGVUaW1lIjoiMjAxOS0wOC0yMVQxODo0OTo0Ni4yMjc2MDUxKzA4OjAwIn0.2ceA8ywKYWfeJr0WZ2Uir7pUSQCuJbB_TRjpNtDIkwk",
            });

        }
    }
}