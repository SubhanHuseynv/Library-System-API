using LibrarySystem.Application.Dtos.Customers;
using LibrarySystem.Application.Interfaces.Repositories;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class CustomerService
{
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<GetAllCustomerDto>> GetAllAsync()
    {
        var customers = await _repository.GetAllAsync();

        return customers.Select(c => new GetAllCustomerDto
        (
            Id: c.Id,
            Name: c.Name
        )).ToList();
    }
}
