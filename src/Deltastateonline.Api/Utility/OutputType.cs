using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Deltastateonline.Utility
{
    public class OutputType
    {
        
        public IActionResult HttpResponse { get; set; }

        [ServiceBusOutput("outbound", Connection = "ServiceBusConnection")]
        public string? ServiceBusMessage { get; set; }

        
    }
}