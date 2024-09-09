

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Deltastateonline.Utility
{
    public class MultipleOutputs<T>
    {
        [ServiceBusOutput("outbound", Connection = "ServiceBusConnection")]
        public T ServiceBusMessage { get; set; }

        public HttpResponseData HttpResponse { get; set; }
    }
}
