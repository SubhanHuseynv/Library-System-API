using LibrarySystem.Application.Dtos.OrderItems;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _service;
        public OrderItemsController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostOrderItemDto itemDto)
        {
            await _service.PostAsync(itemDto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, PutOrderItemDto itemDto)
        {
            await _service.PutAsync(id, itemDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
