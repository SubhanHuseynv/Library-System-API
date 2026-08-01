using AutoMapper;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using MediatR;
using LibrarySystem.Application.Dtos.Authors;

namespace LibrarySystem.Application.Features.Queries.Authors.GetAllAuthor;

public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQueryRequest, GetAllAuthorQueryResponse>
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;
    public GetAllAuthorQueryHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetAllAuthorQueryResponse> Handle(GetAllAuthorQueryRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Author> authors = await _repository.GetAllAsync();
        return new GetAllAuthorQueryResponse()
        {
           GetAllAuthors = authors.Select(a=>new GetAllAuthorDto(
               Id: a.Id,
               Name: a.Name
               )).ToList()
        };
    }
}
