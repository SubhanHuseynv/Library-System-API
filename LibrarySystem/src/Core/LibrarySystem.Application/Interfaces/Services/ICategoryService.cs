using LibrarySystem.Application.Dtos.Categories;

namespace LibrarySystem.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<GetAllCategoryDto>> GetAllAsync();
    Task<GetByIdCategoryDto> GetByIdAsync(long id);
    Task PostAsync(PostCategoryDto postCategoryDto);
    Task PutAsync(long id, PutCategoryDto putCategoryDto);
    Task DeleteAsync(long id);
}
