using AutoMapper;
using LibrarySystem.Application.Dtos.Books;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.MappingProfiles
{
    internal class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, GetBookInMemberDto>();
            CreateMap<Book, GetBookInAuthorDto>();

            CreateMap<Book, GetBookInCategoryDto>();
                
        }
    }
}
