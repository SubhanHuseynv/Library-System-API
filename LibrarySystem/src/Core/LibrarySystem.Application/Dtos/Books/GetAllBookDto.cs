using LibrarySystem.Application.Dtos.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Books
{
    public record GetAllBookDto
    (
        long Id,
        string Name
        );
}
