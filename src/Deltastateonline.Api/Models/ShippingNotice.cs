using System;
using System.Collections.Generic;

namespace Deltastateonline.Models
{
    public class ShippingNotice
    {
        public string ShipmentId { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public List<Product> Products { get; set; }
        public string CarrierName { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class Product
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
