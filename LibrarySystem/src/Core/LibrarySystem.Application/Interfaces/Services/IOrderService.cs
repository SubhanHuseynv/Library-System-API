using LibrarySystem.Application.Dtos.Order;
using LibrarySystem.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IReadOnlyList<GetAllOrderDto>> GetAllAsync(GetAllOrderQuery query);
        Task<GetByIdOrderDto> GetByIdAsync(long id);
        Task PostAsync(PostOrderDto orderDto);
        Task PutAsync(long id, PutOrderDto orderDto);
        Task DeleteAsync(long id);

    }   
}
