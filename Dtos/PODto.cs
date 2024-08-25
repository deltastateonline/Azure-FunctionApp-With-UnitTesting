using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Deltastateonline.Dtos
{

    public class PODto
    {
        
        [OpenApiProperty(Description ="The product name is required.",Nullable = false)]
        //[Newtonsoft.Json.JsonProperty("productName", Required = Newtonsoft.Json.Required.Always)]        
        [Required(ErrorMessage = "ProductName is required")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }

    }
}