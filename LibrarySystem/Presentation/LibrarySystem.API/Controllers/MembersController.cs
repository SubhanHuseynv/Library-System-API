using LibrarySystem.Application.Dtos;
using LibrarySystem.Application.Dtos.Members;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;
    public MembersController(IMemberService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllMembers());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _service.GetByIdMember(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(PostMemberDto memberDto)
    {
        await _service.PostMember(memberDto);
        return Created();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, PutMemberDto memberDto)
    {
        await _service.PutMember(id, memberDto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteMember(id);
        return NoContent();
    }
}
