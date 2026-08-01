using AutoMapper;
using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using MediatR;

namespace LibrarySystem.Application.Features.Queries.Authors.GetByIdAuthor;

public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQueryRequest, GetByIdAuthorQueryResponse>
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdAuthorQueryHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetByIdAuthorQueryResponse> Handle(GetByIdAuthorQueryRequest request, CancellationToken cancellationToken)
    {
       Author? author = await _repository.GetByIdAsync(request.Id,nameof(Author.Books));
        if (author is null) throw new NotFoundException(nameof(Author), request.Id.ToString());

        return new()
        {
            //GetAuthor = _mapper.Map<GetByIdAuthorDto>(author)
            GetAuthor = new GetByIdAuthorDto(
                Id: author.Id,
                Name: author.Name,
                GetBook: author.Books.Select(b => new GetBookInAuthorDto(
                    Name: b.Name,
                    Description: b.Description,
                    TotalCount: b.TotalCount
                    )).ToList()
                )
        };
    }
}
