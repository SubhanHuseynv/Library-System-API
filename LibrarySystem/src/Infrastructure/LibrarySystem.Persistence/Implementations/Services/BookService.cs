using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using MovieAPI.Application.Exceptions;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    public BookService(IBookRepository repository, IAuthorRepository authorRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
    }

    public async Task<IReadOnlyList<GetAllBookDto>> GetAllBooks()
    {
        IReadOnlyList<Book> books = await _repository.GetAllAsync();
        return books.Select(b => new GetAllBookDto(
            Id:b.Id,
            Name: b.Name
            )).ToList();
    }

    public async Task<GetByIdBookDto> GetByIdBook(long id)
    {
        Book? book = await _repository.GetByIdAsync(id, nameof(Book.Author));
        if (book is null) throw new NotFoundException("Entity not found");

        return new GetByIdBookDto(
            Id: book.Id,
            Name: book.Name,
            Description: book.Description,
            TotalCount: book.TotalCount,
            GetAuthor: new GetAuthorInBookDto(
                Id: book.Author.Id,
                Name: book.Author.Name
                ));
    }

    public async Task PostBook(PostBookDto bookDto)
    {
        bool resultName = await _repository.AnyAsync(b=>b.Name == bookDto.Name);
        if (resultName) throw new ConflictException("Name already exists");

        if (!await _authorRepository.AnyAsync(a => a.Id == bookDto.AuthorId))
            throw new NotFoundException("Author not found");

        _repository.Add(
            new Book
            {
                Name = bookDto.Name,
                Description = bookDto.Description,
                TotalCount = bookDto.TotalCount,
                AuthorId = bookDto.AuthorId,
                CreatedAt = DateTime.UtcNow
            }
            );
        await _repository.SaveChangesAsync();
    }

    public async Task PutBook(long id, PutBookDto bookDto)
    {
        Book? book = await _repository.GetByIdAsync(id);
        if (book is null) throw new NotFoundException("Entity not found");

        bool resultName = await _repository.AnyAsync(b => b.Name == bookDto.Name && book.Name != bookDto.Name);
        if (resultName) throw new ConflictException("Name already exists");

        if (!await _authorRepository.AnyAsync(a => a.Id == bookDto.AuthorId))
            throw new NotFoundException("Author not found");

        book.Name = bookDto.Name;
        book.Description = bookDto.Description;
        book.TotalCount = bookDto.TotalCount;
        book.AuthorId = bookDto.AuthorId;
        book.UpdatedAt = DateTime.UtcNow;

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
