using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Tokens
{
    public record TokenResponseDto
    (
        string Token,
        DateTime Expiration,
        string RefreshToken
        );
}
