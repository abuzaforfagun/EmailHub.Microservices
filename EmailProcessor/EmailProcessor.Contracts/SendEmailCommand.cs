using System;
using Communication;
using MediatR;

namespace EmailProcessor.Contracts
{
    public class SendEmailCommand: IRequest, IDistributedCommand
    {
        public string SenderEmail { get; set; }
        public string Content { get; set; }
        public string ReciverEmail { get; set; }
    }
}
