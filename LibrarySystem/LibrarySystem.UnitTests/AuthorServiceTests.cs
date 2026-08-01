using AutoMapper;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using Moq;

namespace LibrarySystem.UnitTests;

public class AuthorServiceTests
{
    private readonly IAuthorService _service;
    private readonly IMapper _mapper;
    public AuthorServiceTests(IAuthorService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    [Fact]
    public async Task Test1()
    {
        //Burada bize birbasa service lazimdirki, onun geriye ne donderdiyi bilek lakin bu zaman biz persistence asili olacaqki, buda 
        //architecturani pozur, buna gore de men qerara geldimki, CQRS + Mediatr pattern den istifade edim.
        var getAllmock = new Mock<IAuthorRepository>();
        //getAllmock.Setup(m => m.GetAllAsync()).ReturnsAsync(fakeList);
    }

    List<Author> fakeList = new List<Author>
    {
        new Author()
        {
            Id = 1,
            Name = "Test",
            Books =
            {
                new Book()
                {
                    Id = 1,
                    Name = "Test",
                    TotalCount = 123,
                    Description = "Test"
                }
            }
        },
        new Author()
        {
            Id = 2,
            Name = "Test1",
            Books =
            {
                    new Book()
                {
                    Id = 1,
                    Name = "Test",
                    TotalCount = 123,
                    Description = "Test"
                }
            }
        }
    };
}