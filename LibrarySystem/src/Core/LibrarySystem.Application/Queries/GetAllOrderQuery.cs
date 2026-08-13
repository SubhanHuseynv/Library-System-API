using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Queries
{
    public class GetAllOrderQuery
    {
        public long? CustomerId { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
