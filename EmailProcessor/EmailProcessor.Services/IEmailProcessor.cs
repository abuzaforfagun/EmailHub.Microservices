using System.Threading.Tasks;
using EmailProcessor.Contracts;

namespace EmailProcessor.Services
{
    public interface IEmailProcessor
    {
        Task SendEmailAsync(SendEmailCommand emailDetails, EmailServiceConfiguration configuration);
    }
}
