using MyTestViajaNet.DomainService.Entities;
using MyTestViajaNet.DomainService.Interfaces.Repositories;
using MyTestViajaNet.DomainService.Interfaces.Services;

namespace MyTestViajaNet.DomainService.Services
{
    public class BatePapoOnlineService : ServiceBase<BatePapoOnline> , IBatePapoOnlineService
    {
        private readonly IBatePapoOnlineRepository batePapoOnlineRepository;
        public BatePapoOnlineService(IBatePapoOnlineRepository batePapoOnlineRepository)
            : base(batePapoOnlineRepository)
        {
            this.batePapoOnlineRepository = batePapoOnlineRepository;
        }
    }
}
