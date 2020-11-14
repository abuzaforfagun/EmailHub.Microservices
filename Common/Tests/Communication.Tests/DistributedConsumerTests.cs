using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Communication.Tests
{
    public class DistributedConsumerTests
    {
        [Fact]
        public async Task Process_Should_Call_Mediator_Send()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<object>>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()));
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IMediator)))
                .Returns(mediatorMock.Object);

            var consumer = new DistributedConsumer(loggerMock.Object, It.IsAny<ServiceBusConfiguration>(),
                serviceProviderMock.Object);

            // Act
            await consumer.Process("abc", serviceProviderMock.Object);

            // Assert
            mediatorMock.Verify(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
