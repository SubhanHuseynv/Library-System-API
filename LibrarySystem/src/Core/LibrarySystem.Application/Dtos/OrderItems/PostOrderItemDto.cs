using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Dtos.OrderItems;

public record PostOrderItemDto
(
    long BookId,
    long OrderId,
    int Quantity
    );
