using LibrarySystem.Application.Dtos;
using LibrarySystem.Application.Dtos.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IMemberService
    {
        Task<IReadOnlyList<GetAllMemberDto>> GetAllMembers();
        Task<GetByIdMemberDto> GetByIdMember(long id);
        Task PostMember(PostMemberDto memberDto);
        Task PutMember(long id, PutMemberDto memberDto);
        Task DeleteMember(long id);
    }
}
