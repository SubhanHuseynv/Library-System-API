using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Implementations.Services;
using LibrarySystem.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibrarySystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenHandlerService, TokenHandlerService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();    

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
               opt => opt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,


                   ValidIssuer = configuration["JWT:Issuer"],
                   ValidAudience = configuration["JWT:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecurityKey"])),
                   LifetimeValidator = (_, exp, token, _) => token != null && exp != null ? exp > DateTime.UtcNow : false

               });

            services.Configure<ICloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudinarySettings>(sp =>
            sp.GetRequiredService<IOptions<CloudinarySettings>>().Value);
            return services;
        }
    }
}
