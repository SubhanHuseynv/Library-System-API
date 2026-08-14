using LibrarySystem.Application.Dtos.Customers;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostCustomerDto customerDto)
        {
            await _customerService.PostAsync(customerDto);
            return Created();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] PutCustomerDto customerDto)
        {
            await _customerService.PutAsync(id, customerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _customerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
