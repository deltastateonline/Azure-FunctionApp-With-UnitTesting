using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Company.Function;
using Deltastateonline.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using tfi_test03.Interfaces;
using Xunit;

namespace ShippingNoticeTest
{
    public class ShippingNoticeGetTests
    {
        private readonly Mock<ILogger<ShippingNoticeGet>> _mockLogger;
        private readonly Mock<IShippingNoticeProvider> _mockShippingNoticeProvider;
        private readonly ShippingNoticeGet _function;

        public ShippingNoticeGetTests()
        {
            _mockLogger = new Mock<ILogger<ShippingNoticeGet>>();
            _mockShippingNoticeProvider = new Mock<IShippingNoticeProvider>();
            _function = new ShippingNoticeGet(_mockLogger.Object, _mockShippingNoticeProvider.Object);
        }

        [Fact]
        public async Task Run_ReturnsOkResponse_WithShippingNotices()
        {
            // Arrange
            var expectedNotices = new List<Deltastateonline.Models.ShippingNotice>
            {
                new Deltastateonline.Models.ShippingNotice { ShipmentId = "S001", ExpectedArrival = DateTime.Now.AddDays(3), CarrierName = "FastShip", TrackingNumber = "TRACK001" }
            };
            _mockShippingNoticeProvider.Setup(provider => provider.GetShippingNoticeListAsync()).ReturnsAsync(expectedNotices);

            var context = new Mock<FunctionContext>();
            var request = new Mock<HttpRequestData>(context.Object);
            request.Setup(req => req.CreateResponse()).Returns(new Mock<HttpResponseData>(context.Object).Object);

            // Act
            var response = await _function.Run(request.Object, context.Object);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //var responseBody = await response.ReadAsStringAsync();
            //var actualNotices = JsonConvert.DeserializeObject<List<ShippingNotice>>(responseBody);
            //Assert.Equal(expectedNotices.Count, actualNotices.Count);
        }

        [Fact]
        public async Task Run_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _mockShippingNoticeProvider.Setup(provider => provider.GetShippingNoticeListAsync()).ThrowsAsync(new Exception("Test exception"));

            var context = new Mock<FunctionContext>();
            var request = new Mock<HttpRequestData>(context.Object);
            request.Setup(req => req.CreateResponse()).Returns(new Mock<HttpResponseData>(context.Object).Object);

            // Act
            var response = await _function.Run(request.Object, context.Object);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
