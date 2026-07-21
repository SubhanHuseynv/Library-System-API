using LibrarySystem.Application.Dtos.Books;

namespace LibrarySystem.Application.Dtos.Members;

public record GetByIdMemberDto
(
    int Id,
    string Name,
    ICollection<GetBookInMemberDto> GetBooks
    );
