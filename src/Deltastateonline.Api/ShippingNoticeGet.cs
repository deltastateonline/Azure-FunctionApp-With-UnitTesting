using Deltastateonline.Utility;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;
using tfi_test03.Interfaces;

namespace Company.Function
{
    public class ShippingNoticeGet
    {
        private readonly ILogger<ShippingNoticeGet> _logger;
        private readonly IShippingNoticeProvider _shippingNoticeProvider;

        public ShippingNoticeGet(ILogger<ShippingNoticeGet> logger , IShippingNoticeProvider shippingNoticeProvider)
        {
            _logger = logger;
            _shippingNoticeProvider = shippingNoticeProvider;
        }

        [Function("ShippingNoticeGet")]
        [OpenApiOperation(operationId: "GetShippingNotices", tags: new[] { "Shipping-Notice" }, Summary = "Get all Shipping notices", Description = "This gets the list Shipping Notice.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]        
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Deltastateonline.Models.ShippingNotice>), CustomHeaderType = typeof(CustomResponseHeader), Summary = "Success", Description = "This returns the response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]

        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route ="ShippingNotice")] HttpRequestData req, FunctionContext context)
        {           

            HttpResponseData response = req.CreateResponse();
            response.Headers.Add("x-correlationid", context.InvocationId);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            response.StatusCode = HttpStatusCode.OK;

            var result = await _shippingNoticeProvider.GetShippingNoticeListAsync();

            await response.WriteStringAsync(JsonConvert.SerializeObject(result));
            return response;
        }

        [Function("ShippingNoticeGetById")]
        [OpenApiOperation(operationId: "GetShippingNoticebyId", tags: new[] { "Shipping-Notice" }, Summary = "Get all Shipping notices", Description = "This gets the list Shipping Notice.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]        
        [OpenApiParameter("shipmentId",In = ParameterLocation.Path , Required = true,Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Deltastateonline.Models.ShippingNotice), CustomHeaderType = typeof(CustomResponseHeader), Summary = "Success", Description = "This returns the response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal server error")]

        public async Task<HttpResponseData> GetShippingnNoticeById([HttpTrigger(AuthorizationLevel.Function, "get", Route ="ShippingNotice/{shipmentId}")] HttpRequestData req, string shipmentId , FunctionContext context)
        {           

            HttpResponseData response = req.CreateResponse();
            response.Headers.Add("x-correlationid", context.InvocationId);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            response.StatusCode = HttpStatusCode.OK;

             if(string.IsNullOrEmpty(shipmentId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var result = await _shippingNoticeProvider.GetShippingNotice(shipmentId);

            if (result == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            await response.WriteStringAsync(JsonConvert.SerializeObject(result));
            return response;
        }
    }
}
