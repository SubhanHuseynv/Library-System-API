using LibrarySystem.Application.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<GetByIdOrderDto> GetByIdAsync(long id);
    }
}
