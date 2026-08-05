namespace LibrarySystem.Application.Dtos.Books;

public record PutBookDto
(
    string Name,
    string Description,
    int TotalCount,
    long AuthorId,
    ICollection<long> CategoryIds
    );
