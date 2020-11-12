using System.Linq;
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

            var result = validator.Validate(new SendEmail.Command {Content = "FYI", ToEmail = "hr@reciver.com"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("SenderEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_SenderEmail_IsNot_Email()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmail.Command {Content = "FYI", ToEmail = "hr@reciver.com", SenderEmail = "notaemail"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("SenderEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_ToEmail_IsNot_Email()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmail.Command {Content = "FYI", ToEmail = "notaemail", SenderEmail = "h1@sender.com"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("ToEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_ToEmail_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmail.Command {Content = "FYI", SenderEmail = "hr@sender.com"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("ToEmail", result.Errors.First().PropertyName);
        }

        [Fact]
        public void SendEmail_CommandValidator_Should_Validate_Content_IsNot_Empty()
        {
            var validator = new SendEmail.CommandValidator();

            var result = validator.Validate(new SendEmail.Command {Content = "", SenderEmail = "hr@sender.com", ToEmail = "to@reciver.com"});

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Content", result.Errors.First().PropertyName);
        }
    }
}
