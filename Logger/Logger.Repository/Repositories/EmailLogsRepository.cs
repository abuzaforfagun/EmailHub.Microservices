using System.Threading.Tasks;
using GenericUnitOfWork;
using Logger.Domain.Features.AddLog;
using Logger.Entities;

namespace Logger.Repository.Repositories
{
    public class EmailLogsRepository : AddLog.IRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailLogsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(EmailLog log)
        {
            _unitOfWork.Repository<EmailLog>().Add(log);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
