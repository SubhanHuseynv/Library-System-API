using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.Account
{
    public record RegisterDto
    (
        string Name,
        string Surname,
        string UserName,
        DateTime DateOfBirth,
        string PhoneNumber,
        string IdentityCardNumber,
        string Password
        );
}
