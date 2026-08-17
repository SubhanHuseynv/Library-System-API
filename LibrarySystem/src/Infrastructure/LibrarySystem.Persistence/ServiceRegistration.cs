using Hangfire;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Context;
using LibrarySystem.Persistence.Implementations.Repositories;
using LibrarySystem.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("default")));
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuthorService, AuthorService>();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IMemberService, MemberService>();

        services.AddScoped<AppDbContextInitializer>();

        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderItemService, OrderItemService>();


        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IBackgroundCleanupService, BackgroundCleanupService>();

        services.AddHangfire(opt => opt.UseSqlServerStorage(configuration.GetConnectionString("default")));
        services.AddHangfireServer();

        return services;
    }

    public static async Task<IApplicationBuilder> UseInitializeDbContext(this IApplicationBuilder app, IServiceScope scope)
    {
        var initialize = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initialize.InitializeDb();
        await initialize.InitializeRoles();
        await initialize.InitializeAdmin();

        return app;
    }

    public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", options:
            new DashboardOptions
            {
                Authorization = new[] { new HangfireAdminAuthorization() }
            });
        RecurringJob.AddOrUpdate<IBackgroundCleanupService>(
    "delete-daily-orders",
    service => service.CleanupOrders(),
    Cron.Daily);

        return app;
    }
}
