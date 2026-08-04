using AutoMapper;
using LibrarySystem.Application.Dtos.Categories;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetAllCategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync(
                includes: "BookCategories.Book");

            return _mapper.Map<IReadOnlyList<GetAllCategoryDto>>(categories);
        }

        public async Task<GetByIdCategoryDto> GetByIdAsync(long id)
        {
            var category = await _repository.GetByIdAsync(id, includes: "BookCategories.Book.Author");
            if (category is null)
                throw new NotFoundException($"Category with ID {id} not found.");
            return _mapper.Map<GetByIdCategoryDto>(category);
        }
        
        public async Task PostAsync(PostCategoryDto postCategoryDto)
        {
            bool resultName = await   _repository.AnyAsync(c => c.Name == postCategoryDto.Name);
            if(resultName) throw new ConflictException($"Category with name {postCategoryDto.Name} already exists.");

            var category = _mapper.Map<Domain.Entities.Category>(postCategoryDto);

            _repository.Add(category);
            await _repository.SaveChangesAsync();
        }

        public async Task PutAsync(long id, PutCategoryDto putCategoryDto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null)
                throw new NotFoundException($"Category with ID {id} not found.");
            bool resultName = await _repository.AnyAsync(c => c.Name == putCategoryDto.Name && c.Id != id);
            if(resultName) throw new ConflictException($"Category with name {putCategoryDto.Name} already exists.");
            _repository.Update(_mapper.Map( putCategoryDto, category));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null)
                throw new NotFoundException($"Category with ID {id} not found.");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

    }
}
