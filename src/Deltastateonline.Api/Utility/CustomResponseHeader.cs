
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.OpenApi.Models;

namespace Deltastateonline.Utility
{

    public class CustomResponseHeader : IOpenApiCustomResponseHeader
    {
        public Dictionary<string, OpenApiHeader> Headers { get ; set; }=
        new Dictionary<string, OpenApiHeader>(){

            {
                "x-correlationid",
                new OpenApiHeader{
                    Description = "Correlation ID",
                    Schema = new OpenApiSchema(){Type = "string"}
                }
            }
        };
    }
}