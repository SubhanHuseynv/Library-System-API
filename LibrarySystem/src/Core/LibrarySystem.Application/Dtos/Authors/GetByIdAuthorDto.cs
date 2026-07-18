using LibrarySystem.Application.Dtos.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Authors
{
    public record GetByIdAuthorDto
    (
        long Id,
        string Name,
        IReadOnlyList<GetBookInAuthorDto> GetBook
        );
}
