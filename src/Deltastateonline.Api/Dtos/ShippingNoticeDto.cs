using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Deltastateonline.Dtos
{
    public class ShippingNoticeDto
    {
        public string ShipmentId { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public List<ProductDto> Products { get; set; }
        public string CarrierName { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class ProductDto
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}