using LibrarySystem.Application.Dtos.OrderItems;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Implementations.Services;
using Moq;

namespace LibrarySystem.Application.UnitTests;

public class OrderItemUnitTests
{
    private readonly Mock<IOrderItemRepository> _mockRepo;
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<IOrderRepository> _mockOrderRepository;

    public OrderItemUnitTests()
    {
        _mockRepo = new Mock<IOrderItemRepository>();
        _mockBookRepository = new Mock<IBookRepository>();
        _mockOrderRepository = new Mock<IOrderRepository>();

    }

    [Fact]
    public async Task CreateOrderItemTest()
    {

        //Range
        var dto = new PostOrderItemDto(
            999999,
            999999,
            999999
            );

        var service = new OrderItemService(_mockRepo.Object,
            _mockBookRepository.Object,
            _mockOrderRepository.Object);

        //Action
        await Assert.ThrowsAsync<NotFoundException>(
            () => service.PostAsync(dto)
            );

        //Assert
        _mockRepo.Verify(m => m.Add(It.IsAny<OrderItem>()), Times.Never());
        _mockRepo.Verify(m => m.SaveChangesAsync(), Times.Never());
    }
}
