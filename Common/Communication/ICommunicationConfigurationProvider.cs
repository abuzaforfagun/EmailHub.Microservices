namespace Communication
{
    public interface ICommunicationConfigurationProvider
    {
        string GetQueueName<T>(T item);
    }
}
