using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Interfaces.Services;
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
    public async Task<IActionResult> GetAll([FromQuery] string? filter, int conSort, int page, int take )
    {
        return Ok(await _service.GetAllBooks(filter,conSort, page, take));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _service.GetByIdBook(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(PostBookDto bookDto)
    {
        await _service.PostBook(bookDto);
        return Created();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id ,PutBookDto bookDto)
    {
        await _service.PutBook(id,bookDto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteBook(id);
        return NoContent();
    }
}
