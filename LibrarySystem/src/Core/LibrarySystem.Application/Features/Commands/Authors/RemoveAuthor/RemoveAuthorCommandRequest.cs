using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Features.Commands.Authors.RemoveAuthor
{
    public class RemoveAuthorCommandRequest : IRequest<RemoveAuthorCommandResponse>
    {
        public long Id { get; set; }
    }
}
