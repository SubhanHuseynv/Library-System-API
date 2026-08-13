using LibrarySystem.Application.Dtos.OrderItems;

namespace LibrarySystem.Application.Dtos.Order;

public record GetByIdOrderDto
(
    long Id,
    string CustomerName,
    ICollection<GetOrderItemInOrderDto> OrderItems,
    DateTime CreatedAt,
    decimal TotalPrice

    );
