using MyTestViajanet.AppService.Interfaces;
using MyTestViajaNet.DomainService.Entities;
using MyTestViajaNet.DomainService.Interfaces.Services;

namespace MyTestViajanet.AppService
{
    public class BatePapoOnlineAppService : AppServiceBase<BatePapoOnline>, IBatePapoOnlineAppService
    {
        private readonly IBatePapoOnlineService batePapoOnlineService;
        public BatePapoOnlineAppService(IBatePapoOnlineService batePapoOnlineService)
            : base(batePapoOnlineService)
        {
            this.batePapoOnlineService = batePapoOnlineService;
        }
    }
}
