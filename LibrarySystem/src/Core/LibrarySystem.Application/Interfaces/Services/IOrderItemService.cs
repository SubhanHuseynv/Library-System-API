using LibrarySystem.Application.Dtos.OrderItems;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<GetByIdOrderItemDto> GetByIdAsync(long id);
    Task PostAsync(PostOrderItemDto orderItemDto);
    Task PutAsync(long id, PutOrderItemDto orderItemDto);
    Task DeleteAsync(long id);
}
