using LibrarySystem.Application.Dtos.Customers;

namespace LibrarySystem.Application.Interfaces.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<GetAllCustomerDto>> GetAllAsync();
    Task PostAsync(PostCustomerDto customerDto);
    Task PutAsync(long id, PutCustomerDto customerDto);
    Task DeleteAsync(long id);
}
