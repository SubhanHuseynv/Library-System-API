using FluentValidation;
using LibrarySystem.Application.Dtos.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Validators.BookValidator
{
    public class PostBookValidator : AbstractValidator<PostBookDto>
    {
        public PostBookValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MinimumLength(2).MaximumLength(256);
            RuleFor(b => b.Description).NotEmpty();
            RuleFor(b => b.TotalCount).NotEmpty().GreaterThan(0);
            RuleFor(b => b.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}
