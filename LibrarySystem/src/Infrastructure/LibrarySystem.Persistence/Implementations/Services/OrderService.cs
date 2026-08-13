using LibrarySystem.Application.Dtos.Order;
using LibrarySystem.Application.Dtos.OrderItems;
using LibrarySystem.Application.Exceptions;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Application.Queries;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Persistence.Implementations.Services;

internal class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    public OrderService(IOrderRepository repository, ICustomerRepository customerRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
    }

    public async Task<IReadOnlyList<GetAllOrderDto>> GetAllAsync(GetAllOrderQuery query)
    {

        var orders = await _repository.GetAllAsync(includes: $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Book)}",
            filter: o => o.CustomerId == query.CustomerId);



        return orders.Select(
            o =>new GetAllOrderDto(
                Id : o.Id,
                TotalBookCount: o.OrderItems.Sum(oi => oi.Quantity),
                TotalPrice: o.TotalPrice
            )).ToList();
    }

    public async Task<GetByIdOrderDto> GetByIdAsync(long id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) throw new NotFoundException("Order not found");


        return new GetByIdOrderDto(
        
            Id : order.Id,
            CreatedAt : order.CreatedAt,
            TotalPrice : order.TotalPrice,
            CustomerName : order.Customer.Name,
            OrderItems : order.OrderItems.Select(oi => new GetOrderItemInOrderDto(
                Id: oi.Id,
                Quantity: oi.Quantity,
                BookName: oi.Book.Name,
                Price: oi.Book.Price,
                UnitPrice: oi.Quantity * oi.Book.Price
                )).ToList()
        );
    }

    public async Task PostAsync(PostOrderDto orderDto)
    {
        Customer? customer = await _customerRepository.GetByIdAsync(orderDto.CustomerId);
        if(customer is null) throw new NotFoundException("CustomerId not found");

        _repository.Add(new Order()
        {
            CustomerId = orderDto.CustomerId,
            TotalPrice = 0
        });

        await _repository.SaveChangesAsync();
    }

    public async Task PutAsync(long id,PutOrderDto orderDto)
    {
        Order? order = await _repository.GetByIdAsync(id);
        if(order is null) throw new NotFoundException("Order not found");

        Customer? customer = await _customerRepository.GetByIdAsync(orderDto.CustomerId);
        if (customer is null) throw new NotFoundException("CustomerId not found");

        order.CustomerId = orderDto.CustomerId;
        _repository.Update(order);

        await _repository.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(long id)
    {
        Order? order = await _repository.GetByIdAsync(id);
        if(order is null) throw new NotFoundException("Order not found");
        _repository.Delete(order);
        await _repository.SaveChangesAsync();
    }

}
