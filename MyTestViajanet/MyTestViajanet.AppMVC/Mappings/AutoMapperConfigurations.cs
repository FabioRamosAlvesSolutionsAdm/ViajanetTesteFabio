using AutoMapper;

namespace MyTestViajanet.AppMVC.AutoMapper
{
    public static class AutoMapperConfigurations
    {
        public static void Initialize()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.AddProfiles(MyTestViajaNet.Kernel.IoC.AutoMapperConfigurations.GetAutoMapperProfiles());
            });
        }
    }
}
