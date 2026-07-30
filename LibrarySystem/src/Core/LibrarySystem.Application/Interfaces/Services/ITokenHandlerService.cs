using LibrarySystem.Application.Dtos.Tokens;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface ITokenHandlerService
    {
        TokenResponseDto CreateAccesToken(AppUser user,IEnumerable<string> roles,int minutes);
        string CreateRefreshToken();
    }
}