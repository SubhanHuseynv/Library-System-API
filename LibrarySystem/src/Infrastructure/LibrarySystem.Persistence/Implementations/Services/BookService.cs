using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Application.Exceptions;
using System.Linq.Expressions;
using LibrarySystem.Application.Common;
using LibrarySystem.Application.Dtos.Categories;
using LibrarySystem.Application.Queries;
using LibrarySystem.Persistence.Utilities.Validators;
using LibrarySystem.Persistence.Utilities.Enums;
using LibrarySystem.Application.Dtos.File;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICloudinaryService _cloudinaryService;
    public BookService(IBookRepository repository,
        IAuthorRepository authorRepository,
        ICategoryRepository categoryRepository,
        ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IReadOnlyList<GetAllBookDto>> GetAllBooks(
         GetAllBookQuery query
        )
    {
        List<Expression<Func<Book, bool>>>? chosenFilter = new();
        if (query.Filter is not null)
        {
            chosenFilter.Add(i => i.Name.Contains(query.Filter));
        }
        if (query.MinPrice > 0)
        {
            chosenFilter.Add(i => i.Price >= query.MinPrice);
        }
        if (query.MaxPrice > 0)
        {
            chosenFilter.Add(i => i.Price <= query.MaxPrice);
        }

        Expression<Func<Book, object>>? sort = null;
        bool isDesc = false;
        if (query.ConSort > 0)
        {
            switch (query.ConSort)
            {
                case (int)ConSort.AscendingName:
                    sort = i => i.CreatedAt;
                    break;
                case (int)ConSort.DescendingName:
                    sort = i => i.CreatedAt;
                    isDesc = true;
                    break;
                case (int)ConSort.AscendingCreatedAt:
                    sort = i => i.CreatedAt;
                    break;
                case (int)ConSort.DescendingCreatedAt:
                    sort = i => i.CreatedAt;
                    isDesc = true;
                    break;
                default:
                    sort = i => i.CreatedAt;
                    break;
            }
        }


        IReadOnlyList<Book> books = await _repository.GetAllAsync(chosenFilter,
           sort, query.Page, query.Take, isDesc
            );
        return books.Select(b => new GetAllBookDto(
            Id: b.Id,
            Name: b.Name
            )).ToList();
    }

    public async Task<GetByIdBookDto> GetByIdBook(long id)
    {
        Book? book = await _repository.GetByIdAsync(id
            ,
            includes: ["BookCategories.Category",
            "BookMembers.Member",
            nameof(Book.Author)]
            );
        if (book is null) throw new NotFoundException("Entity not found");

        return new GetByIdBookDto(
            Id: book.Id,
            Name: book.Name,
            Description: book.Description,
            TotalCount: book.TotalCount,
            GetAuthor: new GetAuthorInBookDto(
                Id: book.Author.Id,
                Name: book.Author.Name
                ),
            GetCategories: book.BookCategories.
            Select(bc => new GetCategoryInBookDto(
                bc.Category.Id,
                bc.Category.Name)
            ).ToList());
    }

    public async Task PostBook(PostBookDto bookDto)
    {
        bool resultName = await _repository.AnyAsync(b => b.Name == bookDto.Name);
        if (resultName) throw new ConflictException("Name already exists");

        if (!await _authorRepository.AnyAsync(a => a.Id == bookDto.AuthorId))
            throw new NotFoundException("Author not found");

        var distinctCategoryIds = bookDto.CategoryIds.Distinct().ToList();
        List<Expression<Func<Category, bool>>>? filters = new()
        {
            b => b.BookCategories.Any(bc => distinctCategoryIds.Contains(bc.CategoryId))
        };
        var existedCIds = await _categoryRepository.GetAllAsync(filters: filters);
        if (existedCIds.Count != distinctCategoryIds.Count) throw new NotFoundException("CategoryIds not found");

        _repository.Add(
            new Book
            {
                Name = bookDto.Name,
                Description = bookDto.Description,
                TotalCount = bookDto.TotalCount,
                AuthorId = bookDto.AuthorId,
                BookCategories = distinctCategoryIds.Select(
                    ci => new BookCategory()
                    {
                        CategoryId = ci
                    }
                    ).ToList()
            }
            );
        await _repository.SaveChangesAsync();
    }

    public async Task PutBook(long id, PutBookDto bookDto)
    {
        Book? book = await _repository.GetByIdAsync(id, "BookCategories.Category");
        if (book is null) throw new NotFoundException("Entity not found");

        bool resultName = await _repository.AnyAsync(b => b.Name == bookDto.Name && book.Name != bookDto.Name);
        if (resultName) throw new ConflictException("Name already exists");

        if (!await _authorRepository.AnyAsync(a => a.Id == bookDto.AuthorId))
            throw new NotFoundException("Author not found");

        var distinctCategoryIds = bookDto.CategoryIds.Distinct().ToList();
        List<Expression<Func<Category, bool>>>? filters = new()
        {
            b => b.BookCategories.Any(bc => distinctCategoryIds.Contains(bc.CategoryId))
        };
        var existedCIds = await _categoryRepository.GetAllAsync(filters: filters);
        if (existedCIds.Count != distinctCategoryIds.Count) throw new NotFoundException("CategoryIds not found");


        book.Name = bookDto.Name;
        book.Description = bookDto.Description;
        book.TotalCount = bookDto.TotalCount;
        book.AuthorId = bookDto.AuthorId;

        //ilk once db dakilari silim daha sonra ise, elave edim
        //biri var hamisini silim daha sonra ise hamisini elave edim
        //Ama en duzgunu db-da olmayanlari silim, dto da olmayanlari elave edim

        var removedCategories = book.BookCategories.Where(bc => !distinctCategoryIds.Contains(bc.CategoryId)).ToList();
        foreach (var rmc in removedCategories)
        {
            book.BookCategories.Remove(rmc);
        }

        var addedCategories = distinctCategoryIds.Where(ci => !book.BookCategories.Any(bc => bc.CategoryId == ci)).ToList();
        if (addedCategories is not null)
        {
            addedCategories.Select(ac => new BookCategory()
            {
                CategoryId = ac,
                BookId = book.Id
            });
        }


        _repository.Update(book);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteBook(long id)
    {
        Book? book = await _repository.GetByIdAsync(id);
        if (book is null) throw new NotFoundException("Entity not found");

        if (!string.IsNullOrEmpty(book.PublicId))
            await _cloudinaryService.DeleteImageAsync(book.PublicId);

        _repository.Delete(book);
        await _repository.SaveChangesAsync();
    }

    public async Task UploadImage(long id, UploadImageInBookDto uploadDto)
    {
        Book? book = await _repository.GetByIdAsync(id);
        if (book is null) throw new NotFoundException("Book id not found");

        if (!uploadDto.image.FileTypeValidator("image"))
            throw new UnsupportedFileTypeException("Type is invalid");

        if (!uploadDto.image.FileSizeValidator(3, SizeType.MB))
            throw new FileTooLargeException("Size is invalid");

        if (!string.IsNullOrEmpty(book.PublicId))
            await _cloudinaryService.DeleteImageAsync(book.PublicId);

        UploadImageDto imageDto = await _cloudinaryService.ImageUploadAsync(uploadDto.image);
        book.PublicId = imageDto.PublicId;
        book.SecureUrl = imageDto.SecureUrl;

        _repository.Update(book);
        await _repository.SaveChangesAsync();
    }

    public async Task<GetImageInBookDto> GetImage(long id)
    {
        Book? book = await _repository.GetByIdAsync(id);
        if (book is null) throw new NotFoundException("Book not found");

        return new(book.SecureUrl);
    }
}
