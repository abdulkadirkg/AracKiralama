//using Core.CrossCuttingConcerns.Caching;
//using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            // uygulama seviyesinde bağımlılıklarımızı çözeceğimiz alan
            serviceCollection.AddMemoryCache(); // dotnetin kendisi otomatik injection yapıyor yani IMemoryCachenin karşılığı var - MemoryCacheMAnagerdeki
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
