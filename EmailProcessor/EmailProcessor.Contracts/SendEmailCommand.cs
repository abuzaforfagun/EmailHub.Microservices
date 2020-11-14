using Communication;
using MediatR;

namespace EmailProcessor.Contracts
{
    public class SendEmailCommand: IRequest, IDistributedCommand
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReciverEmail { get; set; }
        public string ReciverName { get; set; }
    }
}
