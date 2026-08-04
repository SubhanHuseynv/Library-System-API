using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Context;
using LibrarySystem.Persistence.Implementations.Repositories.Generic;

namespace LibrarySystem.Persistence.Implementations.Repositories;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context):base(context)
    {
    }
}
