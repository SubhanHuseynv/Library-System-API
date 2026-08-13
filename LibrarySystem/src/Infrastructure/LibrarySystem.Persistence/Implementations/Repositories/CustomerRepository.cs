using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Context;
using LibrarySystem.Persistence.Implementations.Repositories.Generic;

namespace LibrarySystem.Persistence.Implementations.Repositories;

internal class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext db) : base(db)
    { }
}
