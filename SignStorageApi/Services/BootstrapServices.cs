using System;
using Microsoft.Extensions.DependencyInjection;

namespace SignStorageApi.Services
{
    public static class BootstrapServices
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddSingleton<ISignEngine, SignEngine>();
            services.AddSingleton<ISignService, SignService>();
            services.AddHttpClient<IStorageClientService, StorageClientService>();
        }
    }
}
