using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Domain.Extensions.UnitTests;
using Logger.Contracts;
using Logger.Domain.Features.AddLog;
using Logger.Entities;
using Moq;
using Xunit;

namespace Logger.Tests.Domain.AddLogs
{
    public class HandlerTests
    {
        [Fact]
        public async Task Handler_Should_Call_Repository()
        {
            var repositoryMock = new Mock<AddLog.IRepository>();
            var mapperMock = new Mock<IMapper>();
            var handler = new AddLog.CommandHandler(repositoryMock.Object, mapperMock.Object);

            await handler.Handle(new AddLogCommand());

            repositoryMock.Verify(r => r.AddAsync(It.IsAny<EmailLog>()), Times.Once);
        }
    }
}
