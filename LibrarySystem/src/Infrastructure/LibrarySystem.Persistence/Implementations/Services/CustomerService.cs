using LibrarySystem.Application.Dtos.Customers;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class CustomerService : ICustomerService
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

    public async Task PostAsync(PostCustomerDto customerDto)
    {
        bool resultName = await _repository.AnyAsync(c => c.Name == customerDto.Name);
        if (resultName) throw new ConflictException("Name already exists");


        _repository.Add(new Customer()
        {
            Name = customerDto.Name
        });

        await _repository.SaveChangesAsync();
    }

    public async Task PutAsync(long id, PutCustomerDto customerDto)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null) throw new NotFoundException("Customer not found");
        bool resultName = await _repository.AnyAsync(c => c.Name == customerDto.Name && c.Id != id);
        if (resultName) throw new ConflictException("Name already exists");
        customer.Name = customerDto.Name;
        _repository.Update(customer);
        await _repository.SaveChangesAsync();
    }


    public async Task DeleteAsync(long id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null) throw new NotFoundException("Customer not found");

        _repository.Delete(customer);
        await _repository.SaveChangesAsync();
    }
}
