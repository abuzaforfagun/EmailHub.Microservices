namespace EmailProcessor.Services
{
    public interface IEmailProcessorFactory
    {
        IEmailProcessor GetEmailProcessor(int index);
    }
}
