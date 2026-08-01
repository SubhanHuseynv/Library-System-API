using MediatR;

namespace LibrarySystem.Application.Features.Queries.Authors.GetByIdAuthor;

public class GetByIdAuthorQueryRequest : IRequest<GetByIdAuthorQueryResponse>
{
    public long Id { get; set; }
}
