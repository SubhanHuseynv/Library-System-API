using AutoMapper;
using LibrarySystem.Application.Dtos.Authors;
using LibrarySystem.Application.Features.Commands.Authors.CreateAuthor;
using LibrarySystem.Application.Features.Commands.Authors.RemoveAuthor;
using LibrarySystem.Application.Features.Commands.Authors.UpdateAuthor;
using LibrarySystem.Application.Features.Queries.Authors.GetAllAuthor;
using LibrarySystem.Application.Features.Queries.Authors.GetByIdAuthor;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using Moq;

namespace LibrarySystem.Application.UnitTests;

public class AuthorUnitTests
{
    private List<Author> fakeList;
    private IMapper _mapper;
    private Mock<IAuthorRepository> _mockRepo;

    public AuthorUnitTests()
    {
        MapperConfiguration mconfig = new MapperConfiguration(c =>
        {
            c.CreateMap<Author, GetByIdAuthorDto>();
        });

        _mapper = mconfig.CreateMapper();
        _mockRepo = new Mock<IAuthorRepository>();

        fakeList = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Test",
                Books = new List<Book>()
                {
                    new Book()
                    {
                    Id = 1,
                    Name = "Test",
                    }
                }

            },
            new Author()
            {
                Id = 2,
                Name = "Test2",
                Books = new List<Book>()
                {
                    new Book()
                    {
                    Id = 2,
                    Name = "Test2",
                    TotalCount = 123,
                    Description = "Test",
                    }

                }
            }
      };
    }

    [Fact]
    public async Task GetAllAuthorTest()
    {
        _mockRepo.Setup(m => m.GetAllAsync(null, null, 0, 0, false)).ReturnsAsync(fakeList);

        var handler = new GetAllAuthorQueryHandler(_mockRepo.Object);
        var result = await handler.Handle(new GetAllAuthorQueryRequest(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.GetAllAuthors);
        Assert.Equal(2, result.GetAllAuthors.Count);
        Assert.IsType<List<GetAllAuthorDto>>(result.GetAllAuthors);

        _mockRepo.Verify(m => m.GetAllAsync(null, null, 0, 0, false), Times.Once());
    }

    [Fact]
    public async Task GetByIdAuthorTest()
    {
        _mockRepo.Setup(m => m.GetByIdAsync(It.IsAny<long>(), It.IsAny<string[]>()))
            .ReturnsAsync((long id, string[] includes) => fakeList.FirstOrDefault(i => i.Id == id));
        var handler = new GetByIdAuthorQueryHandler(_mockRepo.Object);

        var result = await handler.Handle(new GetByIdAuthorQueryRequest() { Id = 1 }, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.GetAuthor);
        Assert.IsType<GetByIdAuthorDto>(result.GetAuthor);

        _mockRepo.Verify(m => m.GetByIdAsync(1, It.IsAny<string[]>()), Times.Once());
    }

    [Fact]
    public async Task CreateAuthorTest()
    {
        _mockRepo.Setup(m => m.Add(It.IsAny<Author>()))
            .Callback((Author author) =>
            {
                fakeList.Add(author);
            });

        var handler = new CreateAuthorCommanHandler(_mockRepo.Object);
        var result = await handler.Handle(new CreateAuthorCommandRequest()
        {
            PostAuthor = new PostAuthorDto(Name: "Test3")
        }, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(3, fakeList.Count);
        Assert.Contains(fakeList, i => i.Name == "Test3");

        _mockRepo.Verify(m => m.Add(It.IsAny<Author>()), Times.Once());
    }

    [Fact]
    public async Task UpdateAuthorTest()
    {
        _mockRepo.Setup(m => m.GetByIdAsync(It.IsAny<long>(), It.IsAny<string[]>()))
    .ReturnsAsync((long id, string[] includes) => fakeList.FirstOrDefault(a => a.Id == id));

        _mockRepo.Setup(m => m.Update(It.IsAny<Author>()))
            .Callback((Author author) =>
            {
                fakeList[0].Name = author.Name;
            });


        var handler = new UpdateAuthorCommandHandler(_mockRepo.Object);
        var result = await handler.Handle(new UpdateAuthorCommandRequest()
        {
            Id = 1,
            PutAuthor = new PutAuthorDto(
                Name: "Salam1"
                )

        }, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Salam1", fakeList[0].Name);

        _mockRepo.Verify(m => m.Update(It.IsAny<Author>()), Times.Once());
    }

    [Fact]
    public async Task DeleteAuthorTest()
    {
        _mockRepo.Setup(m => m.GetByIdAsync(It.IsAny<long>(), It.IsAny<string[]>()))
    .ReturnsAsync((long id, string[] includes) => fakeList.FirstOrDefault(a => a.Id == id));
        _mockRepo.Setup(m => m.Delete(It.IsAny<Author>()))
            .Callback((Author author) => fakeList.Remove(author));

        var handler = new RemoveAuthorCommandHandler(_mockRepo.Object);
        var result = handler.Handle(new RemoveAuthorCommandRequest() { Id = 1}, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, fakeList.Count);
        Assert.DoesNotContain(fakeList, a => a.Id == 1);

        _mockRepo.Verify(m => m.Delete(It.IsAny<Author>()), Times.Once());
    }
}