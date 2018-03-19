using MyTestViajaNet.DomainService.Interfaces.Services;
using MyTestViajaNet.DomainService.Services;
using System;
using System.Collections.Generic;

namespace MyTestViajaNet.DomainService.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(IServiceBase<>), typeof(ServiceBase<>));
            dic.Add(typeof(IBatePapoOnlineService), typeof(BatePapoOnlineService));
            return dic;
        }
    }
}
