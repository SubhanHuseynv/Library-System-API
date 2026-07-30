using AutoMapper;
using LibrarySystem.Application.Dtos.Account;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.MappingProfiles;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<RegisterDto, AppUser>();
    }
}
