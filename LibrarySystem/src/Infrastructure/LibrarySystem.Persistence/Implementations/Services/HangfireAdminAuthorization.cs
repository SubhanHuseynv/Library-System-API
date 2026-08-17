using Hangfire.Annotations;
using Hangfire.Dashboard;
using LibrarySystem.Domain.Enums;


namespace LibrarySystem.Persistence.Implementations.Services;

internal class HangfireAdminAuthorization : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    
    {
        var httpContext = context.GetHttpContext();

        var user = httpContext.User;

        if (!user.Identity.IsAuthenticated)
            return false;

        return user.IsInRole(nameof(UserRole.Admin));
    }
}
