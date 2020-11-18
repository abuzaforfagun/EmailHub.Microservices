namespace EmailProcessor.Services
{
    public class EmailServiceConfiguration
    {
        public EmailClient Pepipost { get; set; }
        public EmailClient SenderGrid { get; set; }
    }

    public class EmailClient
    {
        public string AppKey { get; set; }
        public bool IsSandbox { get; set; }
        public string DefaultEmail { get; set; }
    }
}
