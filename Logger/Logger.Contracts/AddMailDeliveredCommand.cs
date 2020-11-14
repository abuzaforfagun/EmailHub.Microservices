using System;
using Communication;
using MediatR;

namespace Logger.Contracts
{
    public class AddMailDeliveredCommand : IRequest, IDistributedCommand
    {
        public Guid Guid { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string ReciverEmail { get; set; }
        public string ReciverName { get; set; }
    }
}
