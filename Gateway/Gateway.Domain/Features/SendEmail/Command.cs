using MediatR;

namespace Gateway.Domain.Features.SendEmail
{
    public partial class SendEmail
    {
        public class Command : IRequest
        {
            public string SenderEmail { get; set; }
            public string Content { get; set; }
            public string ToEmail { get; set; }
        }
    }
}
