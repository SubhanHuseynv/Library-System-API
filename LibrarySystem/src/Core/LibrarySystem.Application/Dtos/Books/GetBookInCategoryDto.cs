namespace LibrarySystem.Application.Dtos.Books;

public record GetBookInCategoryDto
(
    long Id,
    string Name,
    string Description,
    string AuthorName,
    int TotalPageCount
    );
