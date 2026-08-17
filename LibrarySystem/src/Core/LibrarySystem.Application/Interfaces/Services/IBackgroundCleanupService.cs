namespace LibrarySystem.Application.Interfaces.Services;

public interface IBackgroundCleanupService
{
    Task CleanupOrders();
}
