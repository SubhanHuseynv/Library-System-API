namespace LibrarySystem.Application.Dtos.OrderItems
{
    public record GetOrderItemInOrderDto
    (
    long Id,
    int Quantity,
    string BookName,
    decimal Price,
    decimal UnitPrice
    );
}
