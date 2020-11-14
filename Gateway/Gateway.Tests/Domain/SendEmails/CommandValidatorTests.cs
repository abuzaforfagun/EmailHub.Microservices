using System.Linq;
using EmailProcessor.Contracts;
using Gateway.Domain.Features.SendEmail;
using Xunit;

namespace Gateway.Tests.Domain.SendEmails
{
    public class CommandValidatorTests
    {
        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_SenderEmail_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", ReciverEmail = "hr@reciver.com", ReciverName = "Reciver", Subject = "Subject", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("SenderEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_SenderEmail_IsNot_Email()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", ReciverEmail = "hr@reciver.com", SenderEmail = "notaemail", ReciverName = "Reciver", Subject = "Subject", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("SenderEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_ReciverEmail_IsNot_Email()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", ReciverEmail = "notaemail", SenderEmail = "h1@sender.com", ReciverName = "Reciver", Subject = "Subject", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("ReciverEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_ReciverEmail_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", SenderEmail = "hr@sender.com", ReciverName = "Reciver", Subject = "Subject", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("ReciverEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_Content_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "", SenderEmail = "hr@sender.com", ReciverEmail = "to@reciver.com", ReciverName = "Reciver", Subject = "Subject", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Content", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_Subject_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", SenderEmail = "hr@sender.com", ReciverEmail = "to@reciver.com", ReciverName = "Reciver", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Subject", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_ReciverName_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", SenderEmail = "hr@sender.com", ReciverEmail = "to@reciver.com", Subject = "Greetings", SenderName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("ReciverName", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_SenderName_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmailCommand {Content = "FYI", SenderEmail = "hr@sender.com", ReciverEmail = "to@reciver.com", Subject = "Greetings", ReciverName = "Sender"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("SenderName", result.Errors.First().PropertyName);
        }
    }
}
