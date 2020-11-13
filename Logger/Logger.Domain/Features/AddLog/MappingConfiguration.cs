using AutoMapper;
using Logger.Contracts;
using Logger.Entities;

namespace Logger.Domain.Features.AddLog
{
    public partial class AddLog
    {
        public class MappingConfiguration:Profile
        {
            public MappingConfiguration()
            {
                CreateMap<AddLogCommand, EmailLog>();
            }
        }
    }
}
