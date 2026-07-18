using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Books
{
    public record PutBookDto
    (
        string Name,
        string Description,
        int TotalCount,
        long AuthorId
        );
}
