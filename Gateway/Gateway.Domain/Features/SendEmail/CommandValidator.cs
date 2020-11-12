using FluentValidation;

namespace Gateway.Domain.Features.SendEmail
{
    public partial class SendEmail
    {
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.SenderEmail)
                    .NotEmpty()
                    .EmailAddress();
                RuleFor(c => c.Content).NotEmpty();
                RuleFor(c => c.ToEmail)
                    .NotEmpty()
                    .EmailAddress();
            }
        }
    }
    
}
