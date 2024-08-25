using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http;
using Deltastateonline.Dtos;
using Deltastateonline.Models;
using Deltastateonline.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Company.Function
{
    public class ShippingNotice
    {
        private readonly ILogger<ShippingNotice> _logger;

        public ShippingNotice(ILogger<ShippingNotice> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "Create", tags: new[] { "Shipping-Notice" }, Summary = "Create Shipping Notice", Description = "This creates a Shipping Notice.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PODto), Description = "Shipping Notice Details", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(POItem), Summary = "Success", Description = "This returns the response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]

        [Function("ShippingNotice")]
        public async Task<MultipleOutputs<PODto>> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req , FunctionContext context)
        {
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                HttpResponseData  response = req.CreateResponse();
                response.Headers.Add("x-correlationid", context.InvocationId);
                PODto inputRequest = null;
                try
                {
                    inputRequest  = JsonConvert.DeserializeObject<PODto>(requestBody);

                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(inputRequest, null, null);
                    bool isValid = Validator.TryValidateObject(inputRequest, validationContext, validationResults, true);                   
                    

                    if (!isValid)
                    { 
                        response.StatusCode = HttpStatusCode.BadRequest;  
                        await response.WriteAsJsonAsync(validationResults);      

                        return new MultipleOutputs<PODto>{
                            ServiceBusMessage = null,
                            HttpResponse = response
                        };                       
                        
                    }
                }
                catch (System.Exception ex)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    _logger.LogError(ex.Message);
                    return new MultipleOutputs<PODto>{
                        ServiceBusMessage = null,
                        HttpResponse = response
                    };                    
                }     

                response.StatusCode = HttpStatusCode.OK; 
                await response.WriteAsJsonAsync(new {message = "Request Created"});

                return new MultipleOutputs<PODto>{
                    ServiceBusMessage = inputRequest,
                    HttpResponse = response
                }; 

            
        }
    }
}
