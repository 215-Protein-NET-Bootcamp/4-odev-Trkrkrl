using Core.CrossCuttingConcern.Caching;
using Core.CrossCuttingConcern.Caching.Microsoft;
using Core.Utilities.IoC;
using Core.Utilities.URI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolver
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//bu caching için elzem
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //uri ilişki hatası için/-
            //aşağıdaki çözmedi- düz <IUriService,UriManager> hali de çözmedi
            serviceCollection.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext?.Request;
                var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), request?.PathBase);
                return new UriManager(uri);
            });
            //-cache için   
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();//tercih edilen cache aracını değiştirmek istersen sadece
                                                                                //memrocachemanager yerinde ör: Rediscachemanageri yaz 
                                                                                //diğeryerlerde kod yazman gerekmez



        }
    }
}
