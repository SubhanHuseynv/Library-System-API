using LibrarySystem.Application.Dtos;
using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Persistence.Implementations.Services
{
    internal class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetAllAuthorDto>> GetAllAuthors()
        {
            IReadOnlyList<Author> authors = await _repository.GetAllAsync();
            return authors.Select(a => new GetAllAuthorDto(
                id: a.Id,
                Name : a.Name)).ToList();
        }

        public async Task<GetByIdAuthorDto> GetByIdAuthor(long id)
        {
            Author? author = await _repository.GetByIdAsync(id,nameof(Author.Books));
            if (author is null) throw new Exception("Entity not found");

            return new GetByIdAuthorDto(
                id: author.Id,
                Name: author.Name,
                GetBook: author.Books.Select(b =>
                new GetBookInAuthorDto(
                    Name: b.Name,
                    TotalCount: b.TotalCount,
                    Description: b.Description
                    )
                ).ToList());
        }

        public async Task PostAuthor(PostAuthorDto authorDto)
        {
            _repository.Add(new Author
            {
                Name = authorDto.Name,
                CreatedAt =DateTime.UtcNow
            });
            await _repository.SaveChangesAsync();
        }

        public async Task PutAuthor(long id, PutAuthorDto authorDto)
        {
            Author? author = await _repository.GetByIdAsync(id);
            if (author is null) throw new Exception("Entity not found");

            author.Name = authorDto.Name;
            author.UpdatedAt = DateTime.UtcNow;

            _repository.Update(author);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAuthor(long id)
        {
            Author? author  = await _repository.GetByIdAsync(id);
            if (author is null) throw new Exception("Entity not found");

            _repository.Delete(author);
            await _repository.SaveChangesAsync();
        }
    }
}
