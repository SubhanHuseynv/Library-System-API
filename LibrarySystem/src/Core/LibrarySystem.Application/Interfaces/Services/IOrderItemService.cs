using LibrarySystem.Application.Dtos.OrderItems;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<GetByIdOrderItemDTo> GetByIdAsync(long id);
    Task PostAsync(PostOrderItemDto orderItemDto);
    Task PutAsync(long id, PutOrderItemDto orderItemDto);
    Task DeleteAsync(long id);
}
