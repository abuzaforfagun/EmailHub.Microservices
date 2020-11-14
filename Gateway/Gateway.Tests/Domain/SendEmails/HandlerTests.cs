using System.Threading.Tasks;
using AutoMapper;
using Common.Domain.Extensions.UnitTests;
using Communication;
using EmailProcessor.Contracts;
using Gateway.Domain.Features.SendEmail;
using Logger.Contracts;
using Moq;
using Xunit;

namespace Gateway.Tests.Domain.SendEmails
{
    public class HandlerTests
    {
        [Fact]
        public async Task Handler_Should_Send_Messages_To_DistributedSender()
        {
            var distributedSenderMock = new Mock<IDistributedSender>();
            distributedSenderMock.Setup(m => m.SendMessageAsync(It.IsAny<IDistributedCommand>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<AddLogCommand>(It.IsAny<SendEmailCommand>()))
                .Returns(new AddLogCommand());

            var communicationConfigurationMock = new Mock<ICommunicationConfigurationProvider>();
            communicationConfigurationMock.Setup(m => m.GetQueueName(It.IsAny<object>()))
                .Returns("send-email");

            var handler = new SendEmail.CommandHandler(distributedSenderMock.Object, communicationConfigurationMock.Object, mapperMock.Object);

            await handler.Handle(new SendEmailCommand(), default);

            distributedSenderMock.Verify(d => d.SendMessageAsync(It.IsAny<IDistributedCommand>(), It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
