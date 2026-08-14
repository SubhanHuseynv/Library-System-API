using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Application.Queries;
using LibrarySystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;
    public BooksController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBookQuery query)
    {
        return Ok(await _service.GetAllBooks(query));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _service.GetByIdBook(id));
    }
    //[Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    public async Task<IActionResult> Create(PostBookDto bookDto)
    {
        await _service.PostBook(bookDto);
        return Created();
    }
    //[Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, PutBookDto bookDto)
    {
        await _service.PutBook(id, bookDto);
        return NoContent();
    }
    //[Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteBook(id);
        return NoContent();
    }
}
