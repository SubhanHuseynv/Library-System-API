using LibrarySystem.Application.Dtos.Authors;

namespace LibrarySystem.Application.Dtos.Books;

public record GetByIdBookDto
(
    long Id,
    string Name,
    string Description,
    int TotalCount,
    GetAuthorInBookDto GetAuthor
    );
