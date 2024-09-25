using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Deltastateonline.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Company.Function.Tests
{
    public class ShippingNoticeTests
    {
        private readonly Mock<ILogger<ShippingNotice>> _loggerMock;
        private readonly ShippingNotice _shippingNotice;

        public ShippingNoticeTests()
        {
            _loggerMock = new Mock<ILogger<ShippingNotice>>();
            _shippingNotice = new ShippingNotice(_loggerMock.Object);
        }

        [Fact]
        public async Task RunAsync_Should_Return_BadRequest_When_Invalid_Input()
        {
            // Arrange
            var context = new Mock<FunctionContext>();
            var request = new Mock<HttpRequestData>(context.Object);
            var invalidInput = new ShippingNoticeDto(); // Assuming this is invalid
            var requestBody = JsonConvert.SerializeObject(invalidInput);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            request.Setup(r => r.Body).Returns(stream);
            request.Setup(r => r.CreateResponse()).Returns(new Mock<HttpResponseData>(context.Object).Object);

            // Act
            var result = await _shippingNotice.RunAsync(request.Object, context.Object);

            // Assert
            result.HttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.ServiceBusMessage.Should().BeNull();
        }

        [Fact]
        public async Task RunAsync_Should_Return_InternalServerError_On_Exception()
        {
            // Arrange
            var context = new Mock<FunctionContext>();
            var request = new Mock<HttpRequestData>(context.Object);
            var invalidJson = "{ invalid json }";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            request.Setup(r => r.Body).Returns(stream);
            request.Setup(r => r.CreateResponse()).Returns(new Mock<HttpResponseData>(context.Object).Object);

            // Act
            var result = await _shippingNotice.RunAsync(request.Object, context.Object);

            // Assert
            result.HttpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            result.ServiceBusMessage.Should().BeNull();
        }

        [Fact]
        public async Task RunAsync_Should_Return_Ok_When_Valid_Input()
        {
            // Arrange
            var context = new Mock<FunctionContext>();
            var request = new Mock<HttpRequestData>(context.Object);
            var validInput = new ShippingNoticeDto
            {
                // Populate with valid data
            };
            var requestBody = JsonConvert.SerializeObject(validInput);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            request.Setup(r => r.Body).Returns(stream);
            request.Setup(r => r.CreateResponse()).Returns(new Mock<HttpResponseData>(context.Object).Object);

            // Act
            var result = await _shippingNotice.RunAsync(request.Object, context.Object);

            // Assert
            result.HttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ServiceBusMessage.Should().BeEquivalentTo(validInput);
        }
    }
}
