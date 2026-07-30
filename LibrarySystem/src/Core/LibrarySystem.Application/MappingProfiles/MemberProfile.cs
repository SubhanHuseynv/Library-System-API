using AutoMapper;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Application.Dtos.Members;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.MappingProfiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, GetAllMemberDto>();
        CreateMap<Member, GetByIdMemberDto>()
            .ForCtorParam(nameof(GetByIdMemberDto.GetBooks), opt => opt.MapFrom(m => m.BookMembers
            .Select(bm => new GetBookInMemberDto(bm.BookId, bm.Book.Name))));
        CreateMap<PostMemberDto, Member>()
            .ForMember(nameof(Member.BookMembers), opt => opt.MapFrom(pm => pm.BookIds
            .Select(bId => new BookMember { BookId = bId })));
        CreateMap<PutMemberDto, Member>()
            .ForMember(nameof(Member.BookMembers), opt => opt.MapFrom(pm => pm.BookIds
            .Select(bId => new BookMember { BookId = bId })));
    }
}
