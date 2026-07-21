using FluentValidation;
using LibrarySystem.Application.Dtos.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Validators.MemberValidator
{
    public class PostMemberValidator : AbstractValidator<PostMemberDto>
    {
        public PostMemberValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MinimumLength(2).MaximumLength(150);
            RuleFor(m => m.BookIds).NotEmpty().ForEach(bi =>bi.GreaterThan(0));
        }
    }
}
