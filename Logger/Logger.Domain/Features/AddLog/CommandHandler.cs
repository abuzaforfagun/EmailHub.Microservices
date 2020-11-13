using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Logger.Contracts;
using Logger.Entities;
using MediatR;

namespace Logger.Domain.Features.AddLog
{
    public partial class AddLog
    {
        public class CommandHandler : AsyncRequestHandler<AddLogCommand>
        {
            private readonly IRepository _repository;
            private readonly IMapper _mapper;

            public CommandHandler(IRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            protected override async Task Handle(AddLogCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<EmailLog>(request);
                await _repository.AddAsync(entity);
            }
        }
    }
}
