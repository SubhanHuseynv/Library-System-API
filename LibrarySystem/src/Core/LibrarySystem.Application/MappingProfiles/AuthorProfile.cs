using AutoMapper;
using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.MappingProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, GetAllAuthorDto>().ReverseMap();
            CreateMap<Author,GetByIdAuthorDto>().ReverseMap();
        }
    }
}
