namespace LibrarySystem.Application.Dtos.Categories;

public record GetAllCategoryDto
(
    
    long Id,
    string Name,
    int TotalBookCount
);

