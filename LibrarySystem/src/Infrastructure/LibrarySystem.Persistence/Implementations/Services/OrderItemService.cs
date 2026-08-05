using LibrarySystem.Application.Dtos.OrderItems;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repository;
    private readonly IBookRepository _bookRepository;
    public OrderItemService(IOrderItemRepository repository, IBookRepository bookRepository)
    {
        _repository = repository;
        _bookRepository = bookRepository;
    }

    public async Task<GetByIdOrderItemDto> GetByIdAsync(long id)
    {
        var orderItem = await _repository.GetByIdAsync(id, nameof(OrderItem.Book));
        if (orderItem is null) throw new NotFoundException("Entity not found");

        return new GetByIdOrderItemDto(
            Id: orderItem.Id,
            BookName: orderItem.Book.Name,
            Quantity: orderItem.Quantity,
            Price: orderItem.Book.Price,
            UnitPrice: orderItem.UnitPrice
        );
    }

    public async Task PostAsync(PostOrderItemDto orderItemDto)
    {
        Book? book = await _bookRepository.GetByIdAsync(orderItemDto.BookId);
        if (book is null) throw new NotFoundException("BookId not found");

        if (book.Stock < orderItemDto.Quantity)
            throw new ConflictException($"Requested quantity ({orderItemDto.Quantity}) exceeds available stock ({book.Stock}).");

        _repository.Add(new OrderItem()
        {
            Quantity = orderItemDto.Quantity,
            BookId = orderItemDto.BookId,
            UnitPrice = book.Price * orderItemDto.Quantity
        });
        book.Stock = book.Stock - orderItemDto.Quantity;
        _bookRepository.Update(book);

        await _repository.SaveChangesAsync();
    }

    public async Task PutAsync(long id, PutOrderItemDto orderItemDto)
    {
        OrderItem? orderItem = await _repository.GetByIdAsync(id);
        if (orderItem is null) throw new Exception("OrderItem not found");

        Book? book = await _bookRepository.GetByIdAsync(orderItemDto.BookId);
        if (book is null) throw new NotFoundException("BookId not found");

        if (book.Stock < orderItemDto.Quantity)
            throw new ConflictException($"Requested quantity ({orderItemDto.Quantity}) exceeds available stock ({book.Stock}).");

        orderItem.Quantity = orderItemDto.Quantity;
        orderItem.BookId = orderItemDto.BookId;
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
