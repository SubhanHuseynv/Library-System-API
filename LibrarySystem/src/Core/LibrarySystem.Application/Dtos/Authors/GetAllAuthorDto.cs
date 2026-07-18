using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Authors
{
    public record GetAllAuthorDto
    (
        long id,
        string Name
        );
}
