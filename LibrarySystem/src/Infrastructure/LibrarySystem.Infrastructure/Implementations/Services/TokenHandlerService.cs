using LibrarySystem.Application.Dtos.Tokens;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibrarySystem.Infrastructure.Implementations.Services;
internal class TokenHandlerService : ITokenHandlerService
{
    private readonly IConfiguration _configuration;

    public TokenHandlerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResponseDto CreateAccesToken(AppUser user,IEnumerable<string> roles,int minutes)
    {
        List<Claim> claims = new()
      {
          new Claim(ClaimTypes.NameIdentifier, user.Id),
          new Claim(ClaimTypes.Surname, user.Surname),
          new Claim(ClaimTypes.Name,user.UserName),
          new Claim(ClaimTypes.GivenName, user.Name)
      };
        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SymmetricSecurityKey securityKey = new(Encoding.ASCII.GetBytes(_configuration["JWT:SecurityKey"]));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken securityToken = new(
            audience: _configuration["JWT:Audience"],
            issuer: _configuration["JWT:Issuer"],
            expires: DateTime.UtcNow.AddMinutes(minutes),
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: claims
            );

        return new TokenResponseDto(
            new JwtSecurityTokenHandler().WriteToken(securityToken),
            securityToken.ValidTo,
            CreateRefreshToken()
            );
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
