using System.Collections.Generic;

namespace Communication
{
    public class ServiceBusConfiguration
    {
        public string PrimaryKey { get; set; }
        public List<QueueItem> Queues { get; set; }
    }

    public class QueueItem
    {
        public string Name { get; set; }
        public string Contractor { get; set; }
    }
}