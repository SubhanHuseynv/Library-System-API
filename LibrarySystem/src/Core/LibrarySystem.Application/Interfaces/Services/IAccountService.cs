using LibrarySystem.Application.Dtos.Account;
using LibrarySystem.Application.Dtos.Tokens;

namespace LibrarySystem.Persistence.Implementations.Services
{
    public interface IAccountService
    {
        Task<TokenResponseDto> LoginAsync(LoginDto userDto);
        Task LogoutAsync(string refreshToken);
        Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
        Task RegisterAsync(RegisterDto userDto);
    }
}