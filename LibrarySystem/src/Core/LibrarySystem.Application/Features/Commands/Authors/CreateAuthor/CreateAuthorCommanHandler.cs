using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using MediatR;

namespace LibrarySystem.Application.Features.Commands.Authors.CreateAuthor;

public class CreateAuthorCommanHandler : IRequestHandler<CreateAuthorCommandRequest, CreateAuthorCommandResponse>
{
    private readonly IAuthorRepository _repository;
    public CreateAuthorCommanHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }
    public async Task<CreateAuthorCommandResponse> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.AnyAsync(a => a.Name == request.PostAuthor.Name))
            throw new ConflictException(nameof(request.PostAuthor.Name));

        _repository.Add(
            new Author()
            {
                Name = request.PostAuthor.Name,
                CreatedAt = DateTime.UtcNow
            });
        await _repository.SaveChangesAsync();
        return new();

    }
}
