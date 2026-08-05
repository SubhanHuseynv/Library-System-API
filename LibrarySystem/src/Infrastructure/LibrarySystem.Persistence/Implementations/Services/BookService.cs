using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Application.Exceptions;
using System.Linq.Expressions;
using LibrarySystem.Application.Common;
using LibrarySystem.Application.Dtos.Categories;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    public BookService(IBookRepository repository,
        IAuthorRepository authorRepository,
        ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyList<GetAllBookDto>> GetAllBooks(
         string? filter,
        int conSort,
        int page,
        int take
        )
    {
        Expression<Func<Book, bool>>? chosenFilter = null;
        if (filter is not null)
        {
            chosenFilter = i => i.Name.Contains(filter);
        }

        Expression<Func<Book, object>>? sort = null;
        bool isDesc = false;
        if (conSort > 0)
        {
            switch (conSort)
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
           sort, page, take, isDesc
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
        var existedCIds = await _categoryRepository.GetAllAsync(filter: b => b.BookCategories.Any(bc => distinctCategoryIds.Contains(bc.CategoryId)));
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
        Book? book = await _repository.GetByIdAsync(id,"BookCategories.Category");
        if (book is null) throw new NotFoundException("Entity not found");

        bool resultName = await _repository.AnyAsync(b => b.Name == bookDto.Name && book.Name != bookDto.Name);
        if (resultName) throw new ConflictException("Name already exists");

        if (!await _authorRepository.AnyAsync(a => a.Id == bookDto.AuthorId))
            throw new NotFoundException("Author not found");

        var distinctCategoryIds = bookDto.CategoryIds.Distinct().ToList();
        var existedCIds = await _categoryRepository.GetAllAsync(filter: b => b.BookCategories.Any(bc => distinctCategoryIds.Contains(bc.CategoryId)));
        if(existedCIds.Count != distinctCategoryIds.Count) throw new NotFoundException("CategoryIds not found");


        book.Name = bookDto.Name;
        book.Description = bookDto.Description;
        book.TotalCount = bookDto.TotalCount;
        book.AuthorId = bookDto.AuthorId;

        //ilk once db dakilari silim daha sonra ise, elave edim
        //biri var hamisini silim daha sonra ise hamisini elave edim
        //Ama en duzgunu db-da olmayanlari silim, dto da olmayanlari elave edim

        var removedCategories = book.BookCategories.Where(bc => !distinctCategoryIds.Contains(bc.CategoryId)).ToList();
        foreach(var rmc in removedCategories)
        {
            book.BookCategories.Remove(rmc);
        }

        var addedCategories = distinctCategoryIds.Where(ci => !book.BookCategories.Any(bc => bc.CategoryId == ci)).ToList();
        if(addedCategories is not null)
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

        _repository.Delete(book);
        await _repository.SaveChangesAsync();
    }
}
