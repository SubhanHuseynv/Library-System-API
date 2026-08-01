using LibrarySystem.Application.Dtos.Authors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Features.Commands.Authors.CreateAuthor
{
    public class CreateAuthorCommandRequest : IRequest<CreateAuthorCommandResponse>
    {
        public PostAuthorDto PostAuthor { get; set; }
    }
}
