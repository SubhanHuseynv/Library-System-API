using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Context;
using LibrarySystem.Persistence.Implementations.Repositories.Generic;

namespace LibrarySystem.Persistence.Implementations.Repositories;

internal class MemberRepository : Repository<Member>, IMemberRepository
{
    public MemberRepository(AppDbContext context) : base(context)
    {
    }
}
