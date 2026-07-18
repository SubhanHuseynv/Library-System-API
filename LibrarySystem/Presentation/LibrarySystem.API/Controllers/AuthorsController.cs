using LibrarySystem.Application.Dtos;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IAuthorService _service;
        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAuthors());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _service.GetByIdAuthor(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostAuthorDto authorDto)
        {
            await _service.PostAuthor(authorDto);
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, PutAuthorDto authorDto)
        {
            await _service.PutAuthor(id, authorDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAuthor(id);
            return NoContent();
        }
    }
}
