using System.Collections;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Deltastateonline.Models;
using Deltastateonline.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Deltastateonline.Utility;

namespace Deltastateonline.Api
{
    public class PurchaseOrder
    {
        private readonly ILogger<PurchaseOrder> _logger;

        public PurchaseOrder(ILogger<PurchaseOrder> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "Create", tags: new[] { "purchase-orders" }, Summary = "Create Purchase Order", Description = "This creates a purchase order.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PODto), Description = "Purchase Order Details", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(POItem), Summary = "Success", Description = "This returns the response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]
        [Function("PurchaseOrder")]
        [ServiceBusOutput("outbound", Connection = "ServiceBusConnection")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route ="purchaseorder")] HttpRequest req            
        )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                PODto inputRequest  = JsonConvert.DeserializeObject<PODto>(requestBody);

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(inputRequest, null, null);
                bool isValid = Validator.TryValidateObject(inputRequest, validationContext, validationResults, true);
                
                if (!isValid)
                {                  

                    return new BadRequestObjectResult(validationResults); 
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                
                return new InternalServerErrorResult();  
            }           
           return new OkObjectResult("Request Received!");
        }

        [OpenApiOperation(operationId: "GetList", tags: new[] { "purchase-orders" }, Summary = "Get Purchase Orders.", Description = "This returns a list of purchase orders", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<POItem>), Summary = "List of purchase orders", Description = "This returns the response")]
  
        [Function("GetPurchaseOrder")]
        public IActionResult RunGet([HttpTrigger(AuthorizationLevel.Function, "get", Route ="purchaseorder")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            ArrayList orders = new ArrayList();
            orders.Add(new POItem(1, "Laptop", 2, 1500.00m));
            orders.Add(new POItem(2, "Smartphone", 5, 800.00m));
            orders.Add(new POItem(3, "Tablet", 3, 300.00m));
            orders.Add(new POItem(4, "Monitor", 4, 200.00m));
            orders.Add(new POItem(5, "Keyboard", 10, 50.00m));

            return new OkObjectResult(orders);
        }

        [OpenApiOperation(operationId: "GetById", tags: new[] { "purchase-orders" }, Summary = "Get Purchase Orders.", Description = "This returns a purchase order", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(POItem), Summary = "Purchase order item", Description = "Get Purchase Order by Id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Summary = "Bad Request",Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]
        [Function("GetPurchaseOrderItem")]
        public IActionResult RunGetItem([HttpTrigger(AuthorizationLevel.Function, "get", Route ="purchaseorder/{id}")] HttpRequest req , string id)
        {
            _logger.LogInformation("Get Item.");
            if (string.IsNullOrEmpty(id))
            {
                return new BadRequestObjectResult("The 'id' parameter is required.");
            }


            try
            {
                if(int.Parse(id) > 5){
                    throw new Exception("Out of Bound Error.");
                }
                // Your logic here
                return new OkObjectResult(new POItem(int.Parse(id), "Laptop", 2, 1500.00m));
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            
        }
    }
}
