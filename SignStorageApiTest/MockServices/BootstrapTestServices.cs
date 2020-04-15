using System;
using Microsoft.Extensions.DependencyInjection;
using SignStorageApi.Services;

namespace SignStorageApiTest.Services
{
    public static class BootstrapTestServices
    {
        public static void UseTestServices(this IServiceCollection services)
        {
            services.AddSingleton<IStorageClientService, MockStorageClientService>();
        }
    }
}
