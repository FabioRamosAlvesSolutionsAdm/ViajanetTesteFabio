using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MyTestViajaNet.Kernel.IoC
{
    public static class IoCConfigurations
    {
        public static void Configure(IServiceCollection services)
        {
            Configure(services, MyTestViajanet.Infra.Data.Module.GetTypes());
            Configure(services, MyTestViajaNet.DomainService.IoC.Module.GetTypes());
            Configure(services, MyTestViajaNet.AppServices.IoC.Module.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}
