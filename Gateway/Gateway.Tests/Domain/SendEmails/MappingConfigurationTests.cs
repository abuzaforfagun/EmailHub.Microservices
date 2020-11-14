using System;
using AutoMapper;
using EmailProcessor.Contracts;
using Gateway.Domain.Features.SendEmail;
using Logger.Contracts;
using Xunit;

namespace Gateway.Tests.Domain.SendEmails
{
    public class MappingConfigurationTests
    {
        [Fact]
        public void Mapper_Should_Map_SendEmailCommand_To_AddLogCommand()
        {
            var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(new Sendemail.MappingConfiguration()));
            IMapper mapper = new Mapper(mapperCfg);
            var sendEmailCommand = new SendEmailCommand
            {
                SenderName = "Fagun",
                SenderEmail = "mdabuzaforfagun@gmail.com",
                Content = "Sending moq",
                Subject = "Moq",
                ReciverName = "Jack",
                ReciverEmail = "jack@j.com"
            };

            var result = mapper.Map<AddLogCommand>(sendEmailCommand);

            var currentDate = DateTime.Now;
            Assert.Equal(sendEmailCommand.SenderEmail, result.SenderEmail );
            Assert.Equal(sendEmailCommand.ReciverEmail, result.ReciverEmail );
            Assert.Equal(sendEmailCommand.Subject, result.Subject );
            Assert.Equal(currentDate.Date, result.SendOn.Date);
            Assert.Equal(currentDate.Hour, result.SendOn.Hour);
            Assert.Equal(currentDate.Minute, result.SendOn.Minute);
        }
    }
}
