using System;
using System.Threading.Tasks;
using Common.Domain;
using Common.Domain.Extensions.UnitTests;
using EmailProcessor.Contracts;
using EmailProcessor.Domain.Features.SendEmail;
using EmailProcessor.Services;
using Moq;
using Xunit;

namespace EmailProcessor.Tests.Domain.Features.SendEmails
{
    public class HandlerTests
    {
        [Fact]
        public async Task Handler_Should_Invoke_Retry_Helper()
        {
            var emailProcessorMock = new Mock<IEmailProcessor>();
            var factoryMock = new Mock<IEmailProcessorFactory>();
            factoryMock.Setup(m => m.GetEmailProcessor(It.IsAny<int>()))
                .Returns(emailProcessorMock.Object);
            var retryHelperMock = new Mock<IRetryHelper>();
            retryHelperMock.Setup(m => m.InvokeAsync(It.IsAny<Func<int, Task>>()))
                .Returns(Task.CompletedTask);
            var handler = new SendEmail.CommandHandler(factoryMock.Object, retryHelperMock.Object);

            await handler.Handle(new SendEmailCommand());

            retryHelperMock.Verify(r => r.InvokeAsync(It.IsAny<Func<int, Task>>()), Times.AtLeastOnce);
        }
    }
}
