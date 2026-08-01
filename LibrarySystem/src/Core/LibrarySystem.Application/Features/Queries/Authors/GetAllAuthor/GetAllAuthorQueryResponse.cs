using LibrarySystem.Application.Dtos.Authors;

namespace LibrarySystem.Application.Features.Queries.Authors.GetAllAuthor;

public class GetAllAuthorQueryResponse
{
    public List<GetAllAuthorDto> GetAllAuthors { get; set; }
}
