namespace LibrarySystem.Application.Dtos.OrderItems;

public record GetByIdOrderItemDto
(
    long Id,
    int Quantity,
    string BookName,
    decimal Price,
    decimal UnitPrice
    );
