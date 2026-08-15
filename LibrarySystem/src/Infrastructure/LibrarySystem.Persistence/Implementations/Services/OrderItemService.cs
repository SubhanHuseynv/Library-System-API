using LibrarySystem.Application.Dtos.OrderItems;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Persistence.Implementations.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repository;
    private readonly IBookRepository _bookRepository;
    private readonly IOrderRepository _orderRepository;
    public OrderItemService(IOrderItemRepository repository, IBookRepository bookRepository, IOrderRepository orderRepository)
    {
        _repository = repository;
        _bookRepository = bookRepository;
        _orderRepository = orderRepository;
    }

    public async Task<GetByIdOrderItemDTo> GetByIdAsync(long id)
    {
        OrderItem? orderItem = await _repository.GetByIdAsync(id, includes: nameof(orderItem.Book));
        if (orderItem is null) throw new NotFoundException("Order item not found");
        return new GetByIdOrderItemDTo(
            Id: orderItem.Id,
            Quantity: orderItem.Quantity,
            BookName: orderItem.Book.Name,
            UnitPrice: orderItem.UnitPrice
            );
    }

    public async Task PostAsync(PostOrderItemDto orderItemDto)
    {
        Book? book = await _bookRepository.GetByIdAsync(orderItemDto.BookId);
        if (book is null) throw new NotFoundException("BookId not found");

        Order? order = await _orderRepository.GetByIdAsync(orderItemDto.OrderId);
        if(order is null) throw new NotFoundException("OrderId not found");

        if (book.Stock < orderItemDto.Quantity)
            throw new ConflictException($"Requested quantity ({orderItemDto.Quantity}) exceeds available stock ({book.Stock}).");

        _repository.Add(new OrderItem()
        {
            Quantity = orderItemDto.Quantity,
            BookId = orderItemDto.BookId,
            OrderId = orderItemDto.OrderId,
            UnitPrice = book.Price * orderItemDto.Quantity
        });
        book.Stock = book.Stock - orderItemDto.Quantity;
        _bookRepository.Update(book);

        order.TotalPrice += book.Price * orderItemDto.Quantity;
        _orderRepository.Update(order);

        await _repository.SaveChangesAsync();
    }

    public async Task PutAsync(long id, PutOrderItemDto orderItemDto)
    {
        OrderItem? orderItem = await _repository.GetByIdAsync(id);
        if (orderItem is null) throw new Exception("OrderItem not found");

        Book? book = await _bookRepository.GetByIdAsync(orderItem.BookId);
        Order? order = await _orderRepository.GetByIdAsync(orderItem.OrderId);

        if (book.Stock + orderItem.Quantity < orderItemDto.Quantity)
            throw new ConflictException($"Requested quantity ({orderItemDto.Quantity}) exceeds available stock ({book.Stock}).");

        book.Stock = (book.Stock + orderItem.Quantity) - orderItemDto.Quantity;
        _bookRepository.Update(book);

       order.TotalPrice = (order.TotalPrice - orderItem.UnitPrice) + orderItemDto.Quantity * book.Price;
        _orderRepository.Update(order);

        orderItem.Quantity = orderItemDto.Quantity;
        orderItem.UnitPrice = orderItemDto.Quantity * book.Price;

        _repository.Update(orderItem);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
       OrderItem? item =  await _repository.GetByIdAsync(id);
        if (item is null) throw new NotFoundException("Order item not found");

        _repository.Delete(item);
        await _repository.SaveChangesAsync();
    }
}
