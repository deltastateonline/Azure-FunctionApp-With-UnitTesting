using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfi_test03.Interfaces;
using tfi_test03.Providers;

namespace tfi_test03
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<IShippingNoticeProvider, ShippingNoticeProvider>();
            services.AddScoped<IShippingNoticeRepoWrapper, ShippingNoticeRepoWrapper>();
            return services;
        }
    }
}
