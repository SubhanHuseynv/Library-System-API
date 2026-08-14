namespace LibrarySystem.Application.Dtos.Order;

public record GetAllOrderDto
    (
    long Id,
    int TotalBookCount,
    decimal TotalPrice,
    DateTime CreatedAt,
    string CustomerName
    );
