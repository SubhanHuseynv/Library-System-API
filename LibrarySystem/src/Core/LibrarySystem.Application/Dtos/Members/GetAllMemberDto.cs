using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Members
{
    public record GetAllMemberDto
    (
        long Id,
        string Name
        );
}
