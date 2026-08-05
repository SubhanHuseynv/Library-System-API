namespace LibrarySystem.Application.Dtos.OrderItems;

public record PutOrderItemDto
(
    int BookId,
    int Quantity
    );
