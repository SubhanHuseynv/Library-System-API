using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Dtos.OrderItems
{
    public record GetByIdOrderItemDTo
    (
        long Id,
        int Quantity,
        decimal UnitPrice,
        string BookName

    );
}
