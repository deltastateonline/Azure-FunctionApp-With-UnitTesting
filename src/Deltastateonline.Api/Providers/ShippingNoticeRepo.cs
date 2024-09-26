using Deltastateonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfi_test03.Providers
{
    public class ShippingNoticeRepo
    {
        public static List<ShippingNotice> ShippingNotices= new List<ShippingNotice>{
            new ShippingNotice
            {
                ShipmentId = "S001",
                ExpectedArrival = DateTime.Now.AddDays(3),
                Products = new List<Product>
                {
                    new Product { ProductCode = "P001", Description = "Product 1", Quantity = 10 },
                    new Product { ProductCode = "P002", Description = "Product 2", Quantity = 5 }
                },
                CarrierName = "FastShip",
                TrackingNumber = "TRACK001"
            },
            new ShippingNotice
            {
                ShipmentId = "S002",
                ExpectedArrival = DateTime.Now.AddDays(5),
                Products = new List<Product>
                {
                    new Product { ProductCode = "P003", Description = "Product 3", Quantity = 20 },
                    new Product { ProductCode = "P004", Description = "Product 4", Quantity = 15 }
                },
                CarrierName = "QuickCarrier",
                TrackingNumber = "TRACK002"
            }
            ,
            new ShippingNotice
            {
                ShipmentId = "S003",
                ExpectedArrival = DateTime.Now.AddDays(5),
                Products = new List<Product>
                {
                    new Product { ProductCode = "P009", Description = "Product 3", Quantity = 12 },
                    new Product { ProductCode = "P006", Description = "Product 4", Quantity = 17 }
                },
                CarrierName = "QuickCarrier",
                TrackingNumber = "TRACK0033"
            },
        };
    }
}
