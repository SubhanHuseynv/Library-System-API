using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using MediatR;

namespace LibrarySystem.Application.Features.Commands.Authors.RemoveAuthor;

public class RemoveAuthorCommandHandler : IRequestHandler<RemoveAuthorCommandRequest, RemoveAuthorCommandResponse>
{
    private readonly IAuthorRepository _repository;
    public RemoveAuthorCommandHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }
    public async Task<RemoveAuthorCommandResponse> Handle(RemoveAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        Author? author = await _repository.GetByIdAsync(request.Id);
        if (author is null) throw new NotFoundException(nameof(Author), request.Id.ToString());

        _repository.Delete(author);
        await _repository.SaveChangesAsync();

        return new();
    }
}
