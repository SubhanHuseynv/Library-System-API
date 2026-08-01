using LibrarySystem.Application.Dtos.Authors;
using MediatR;

namespace LibrarySystem.Application.Features.Commands.Authors.UpdateAuthor;

public class UpdateAuthorCommandRequest : IRequest<UpdateAuthorCommandResponse>
{
    public long Id { get; set; }
    public PutAuthorDto PutAuthor { get; set; }
}
