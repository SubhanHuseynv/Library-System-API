using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using MediatR;

namespace LibrarySystem.Application.Features.Commands.Authors.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommandRequest, UpdateAuthorCommandResponse>
{
    private readonly IAuthorRepository _repository;
    public UpdateAuthorCommandHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }
    public async Task<UpdateAuthorCommandResponse> Handle(UpdateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        Author? author = await _repository.GetByIdAsync(request.Id);
        if (author is null) throw new NotFoundException(nameof(Author), request.Id.ToString());

        author.Name = request.PutAuthor.Name;

        _repository.Update(author);
        await _repository.SaveChangesAsync();

        return new();
    }
}
