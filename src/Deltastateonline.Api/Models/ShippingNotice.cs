using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Deltastateonline.Models
{
    public class ShippingNotice
    {
        [JsonPropertyName("shipmentId")]
        public string ShipmentId { get; set; }

        [JsonPropertyName("expectedArrival")]
        public DateTime ExpectedArrival { get; set; }

        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }

        [JsonPropertyName("carrierName")]
        public string CarrierName { get; set; }

        [JsonPropertyName("trackingNumber")]
        public string TrackingNumber { get; set; }
    }

    public class Product
    {
        [JsonPropertyName("productCode")]
        public string ProductCode { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
