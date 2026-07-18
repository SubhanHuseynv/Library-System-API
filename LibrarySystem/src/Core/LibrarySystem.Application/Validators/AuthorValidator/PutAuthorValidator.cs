using FluentValidation;
using LibrarySystem.Application.Dtos.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Validators.AuthorValidator
{
    public class PutAuthorValidator : AbstractValidator<PutAuthorDto>
    {
        public PutAuthorValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(2).MaximumLength(150);
        }
    }
}
