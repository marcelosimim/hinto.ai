using Hinto.Domain.Contract;
using Hinto.Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hinto.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHintoAIDomain, HintoAIDomain>();
            return services;
        }
    }
}
