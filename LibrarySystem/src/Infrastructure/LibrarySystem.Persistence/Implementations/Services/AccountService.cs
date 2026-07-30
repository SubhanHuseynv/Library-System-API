using AutoMapper;
using LibrarySystem.Application.Dtos.Account;
using LibrarySystem.Application.Dtos.Tokens;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandlerService _tokenService;
    private readonly IMapper _mapper;

    private const int AccesTokenMinutes = 45;
    private const int AddOnAccesTokenMinutes = 15;

    public AccountService(UserManager<AppUser> userManager, IMapper mapper, ITokenHandlerService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(RegisterDto userDto)
    {
        if (await _userManager.Users.AnyAsync(u => u.IdentityCardNumber == userDto.IdentityCardNumber))
            throw new ConflictException(nameof(userDto.IdentityCardNumber));

        AppUser user = _mapper.Map<AppUser>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            StringBuilder sb = new();
            foreach (var error in result.Errors)
            {
                sb.Append(error.Description);
            }
            throw new Exception(sb.ToString());
        }
        await _userManager.AddToRoleAsync(user, nameof(UserRole.Member));
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto userDto)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName);
        if (user is null) throw new NotFoundException("Username or Password is invalid");

        if (await _userManager.IsLockedOutAsync(user))
            throw new ForbiddenException("Your account has been blocked");

        bool result = await _userManager.CheckPasswordAsync(user, userDto.Password);
        if (!result)
        {
            await _userManager.AccessFailedAsync(user);
            throw new UnauthorizedException("Username or Password is invalid");
        }

        return await _createToken(user);
    }

    public async Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user is null) throw new UnauthorizedException("Refresh token is invalid");

        if (user.RefreshTokenExpiration < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token expired");

        return await _createToken(user);
    }

    public async Task LogoutAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user is null) throw new Exception("Invalid token");

        user.RefreshToken = null;
        user.RefreshTokenExpiration = null;

        await _userManager.UpdateAsync(user);
    }

    private async Task<TokenResponseDto> _createToken(AppUser user)
    {
        TokenResponseDto token = _tokenService.CreateAccesToken(user, await _userManager.GetRolesAsync(user), AccesTokenMinutes);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiration = token.Expiration.AddMinutes(AddOnAccesTokenMinutes);

        await _userManager.UpdateAsync(user);
        return token;
    }
}
