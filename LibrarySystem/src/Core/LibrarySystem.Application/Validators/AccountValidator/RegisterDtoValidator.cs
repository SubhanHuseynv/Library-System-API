using FluentValidation;
using LibrarySystem.Application.Dtos.Account;

namespace LibrarySystem.Application.Validators.AccountValidator;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MinimumLength(2);

        RuleFor(x => x.Surname)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.Now);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+994(50|51|55|70|77|99|10|12)\d{7}$");
                
        RuleFor(x => x.IdentityCardNumber)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);
            
    }
}
