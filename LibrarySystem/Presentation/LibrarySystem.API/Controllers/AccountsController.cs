using LibrarySystem.Application.Dtos.Account;
using LibrarySystem.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm]RegisterDto userDto)
    {
        await _service.RegisterAsync(userDto);
        return Created();
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm]LoginDto userDto)
    {
        return Ok(await _service.LoginAsync(userDto));
    }
    [HttpPost("Refresh")]
    public async Task<IActionResult> LoginByRefresh(string refreshToken)
    {
        return Ok(await _service.RefreshTokenLoginAsync(refreshToken));
    }
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        await _service.LogoutAsync(refreshToken);
        return NoContent();
    }


}
