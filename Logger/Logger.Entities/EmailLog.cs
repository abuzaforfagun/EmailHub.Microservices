using System;

namespace Logger.Entities
{
    public class EmailLog
    {
        public Guid Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReciverEmail { get; set; }
        public DateTime SendOn { get; set; }
    }
}
