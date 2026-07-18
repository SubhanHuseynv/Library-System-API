using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Context;
using LibrarySystem.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Persistence.Implementations.Repositories
{
    internal class AuthorRepository :  Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context):base(context) { }
    }
}
