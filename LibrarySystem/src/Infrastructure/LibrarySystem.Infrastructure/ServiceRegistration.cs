using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandlerService, TokenHandlerService>();

            return services;
        }
    }
}
