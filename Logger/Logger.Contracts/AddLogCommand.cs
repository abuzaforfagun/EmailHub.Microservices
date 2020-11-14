using System;
using Communication;
using MediatR;

namespace Logger.Contracts
{
    public class AddLogCommand : IRequest, IDistributedCommand
    {
        public Guid Id { get; set; }
        public DateTime SendOn { get; set; }
        public string SenderEmail { get; set; }
        public string ReciverEmail { get; set; }
        public string Subject { get; set; }
    }
}
