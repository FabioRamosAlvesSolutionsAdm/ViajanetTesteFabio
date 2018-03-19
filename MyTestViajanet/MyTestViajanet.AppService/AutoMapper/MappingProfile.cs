using AutoMapper;
using MyTestViajanet.AppService.ViewModel;

namespace MyTestViajanet.AppMVC.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BatePapoOnlineViewModel, MyTestViajaNet.DomainService.Entities.BatePapoOnline>().ReverseMap();
        }
    }
}
