using LibrarySystem.Application.Dtos.Books;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IBookService
{
    Task<IReadOnlyList<GetAllBookDto>> GetAllBooks();
    Task<GetByIdBookDto> GetByIdBook(long id);
    Task PostBook(PostBookDto bookDto);
    Task PutBook(long id, PutBookDto bookDto);
    Task DeleteBook(long id);
}
