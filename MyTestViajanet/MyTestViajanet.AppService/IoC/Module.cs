namespace MyTestViajaNet.AppServices.IoC
{
    using MyTestViajanet.AppService;
    using MyTestViajanet.AppService.Interfaces;
    using System;
    using System.Collections.Generic;

    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();

            dic.Add(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
            dic.Add(typeof(IBatePapoOnlineAppService), typeof(BatePapoOnlineAppService));

            return dic;
        }
    }
}
