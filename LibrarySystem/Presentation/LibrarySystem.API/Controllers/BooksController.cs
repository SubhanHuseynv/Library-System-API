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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBookQuery query)
    {
        return Ok(await _service.GetAllBooks(query));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _service.GetByIdBook(id));
    }

    //[Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(PostBookDto bookDto)
    {
        await _service.PostBook(bookDto);
        return Created();
    }

    //[Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, PutBookDto bookDto)
    {
        await _service.PutBook(id, bookDto);
        return NoContent();
    }

    //[Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteBook(id);
        return NoContent();
    }

    [HttpPut("{id}/uploadImage")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadImage(
        long id,
        [FromForm] UploadImageInBookDto uploadImage)
    {
        await _service.UploadImage(id, uploadImage);
        return NoContent();
    }

    [HttpGet("{id}/getImage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage(long id)
    {
        return Ok(await _service.GetImage(id));
    }
}