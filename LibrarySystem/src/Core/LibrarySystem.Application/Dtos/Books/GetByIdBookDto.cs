using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Dtos.Categories;

namespace LibrarySystem.Application.Dtos.Books;

public record GetByIdBookDto
(
    long Id,
    string Name,
    string Description,
    int TotalCount,
    GetAuthorInBookDto GetAuthor,
    ICollection<GetCategoryInBookDto> GetCategories
    );
