using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Persistence.Context;

public sealed class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AppDbContextInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeDb()
    {
        await _context.Database.MigrateAsync();
    }

    public async Task InitializeRoles()
    {
        foreach (var role in Enum.GetValues(typeof(UserRole)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new()
                {
                    Name = role.ToString()
                });
            }
        }
    }

    public async Task InitializeAdmin()
    {
        if(!await _userManager.Users.AnyAsync(u => u.UserName == "Admin"))
        {
            AppUser user = new()
            {
                UserName = "Admin",
                Name = "Admin",
                Surname ="Admin",

            };
            await _userManager.CreateAsync(user,"Admin123.");
            await _userManager.AddToRoleAsync(user, nameof(UserRole.Admin));
        }
    }
}
