namespace LibrarySystem.Application.Dtos.Books;

public record PostBookDto
(
    string Name,
    string Description,
    int TotalCount,
    long AuthorId
    );
