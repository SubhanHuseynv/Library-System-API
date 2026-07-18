using LibrarySystem.Application.Dtos;
using LibrarySystem.Application.Dtos.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<IReadOnlyList<GetAllAuthorDto>> GetAllAuthors();
        Task<GetByIdAuthorDto> GetByIdAuthor(long id);
        Task PostAuthor(PostAuthorDto authorDto);
        Task PutAuthor(long id, PutAuthorDto authorDto);
        Task DeleteAuthor(long id);
    }
}
