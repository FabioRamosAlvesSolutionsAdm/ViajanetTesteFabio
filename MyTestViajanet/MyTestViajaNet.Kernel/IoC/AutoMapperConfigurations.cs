using MyTestViajanet.AppMVC.AutoMapper;
using System;
using System.Collections.Generic;

namespace MyTestViajaNet.Kernel.IoC
{
    public static class AutoMapperConfigurations
    {
        
        public static IEnumerable<Type> GetAutoMapperProfiles()
        {
            var result = new List<Type>();
            result.Add(typeof( MappingProfile));
            return result;
        }
    }
}

