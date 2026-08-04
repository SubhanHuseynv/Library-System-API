using AutoMapper;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Dtos.Categories;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetAllCategoryDto>()
            .ForCtorParam(nameof(GetAllCategoryDto.TotalBookCount), opt => opt.MapFrom(c => c.BookCategories.Count()));
        CreateMap<Category, GetByIdCategoryDto>()
             .ForCtorParam(nameof(GetByIdCategoryDto.TotalBookCount), opt => opt.MapFrom(c => c.BookCategories.Count()))
            .ForCtorParam(nameof(GetByIdCategoryDto.GetBooks), opt => opt.MapFrom(c => c.BookCategories.Select(
                b => new GetBookInCategoryDto(b.Book.Id, b.Book.Name, b.Book.Description, b.Book.Author.Name, b.Book.TotalCount))));
        CreateMap<Category, PostCategoryDto>().ReverseMap();
        CreateMap<Category, PutCategoryDto>().ReverseMap();
    }
}
