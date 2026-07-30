using FluentValidation;
using LibrarySystem.Application.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Validators.AccountValidator
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(256)
                .Matches(@"^[A-Za-z0-9-._@+]*$");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}
