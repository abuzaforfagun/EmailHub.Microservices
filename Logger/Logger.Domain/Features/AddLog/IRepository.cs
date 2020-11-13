using System.Threading.Tasks;
using Logger.Entities;

namespace Logger.Domain.Features.AddLog
{
    public partial class AddLog
    {
        public interface IRepository
        {
            Task AddAsync(EmailLog log);
        }
    }
}
