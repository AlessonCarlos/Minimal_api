namespace MinimalApi.Configuracoes.Swagger;


using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;

public class AllowAnonymousOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAnonymous = context.ApiDescription.CustomAttributes()
            .OfType<AllowAnonymousAttribute>()
            .Any();

            if (hasAnonymous)
            {
            operation.Security.Clear();
            }
            
        }
    }

