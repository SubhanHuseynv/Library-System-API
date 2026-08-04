using LibrarySystem.Application.Dtos.Books;

namespace LibrarySystem.Application.Dtos.Categories;

public record GetByIdCategoryDto
(
    long Id,
    string Name,
    int TotalBookCount,
    ICollection<GetBookInCategoryDto> GetBooks
    );
