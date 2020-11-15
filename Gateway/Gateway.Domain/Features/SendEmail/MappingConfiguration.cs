using System;
using AutoMapper;
using EmailProcessor.Contracts;
using Logger.Contracts;

namespace Gateway.Domain.Features.SendEmail
{
    public partial class Sendemail
    {
        public class MappingConfiguration : Profile
        {
            public MappingConfiguration()
            {
                CreateMap<SendEmailCommand, AddLogCommand>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(d => Guid.NewGuid()))
                    .ForMember(d => d.SendOn, opt => opt.UseValue(DateTime.Now));
            }
        }
    }
    
}
