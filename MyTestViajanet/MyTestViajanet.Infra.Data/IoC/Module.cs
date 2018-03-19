using MyTestViajanet.Infra.Data.Repositories;
using MyTestViajaNet.DomainService.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace MyTestViajanet.Infra.Data
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(IRepositoryBase<>), value: typeof(RepositoryBase<>));
            dic.Add(typeof(IBatePapoOnlineRepository), typeof(BatePapoOnlineRepository));
            return dic;
        }
    }
}
