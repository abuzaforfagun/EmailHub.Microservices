using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EmailProcessor.Services
{
    public class EmailProcessorFactory : IEmailProcessorFactory
    {
        private readonly List<IEmailProcessor> _emailProcessors = new List<IEmailProcessor>();

        public EmailProcessorFactory(IServiceProvider serviceProvider)
        {
            using (var serviceProviderScope = serviceProvider.CreateScope())
            {
                var processor = serviceProviderScope.ServiceProvider.GetServices<IEmailProcessor>();
                _emailProcessors.AddRange(processor);
            }
        }

        public IEmailProcessor GetEmailProcessor(int index)
        {
            return _emailProcessors[index];
        }
    }
}
