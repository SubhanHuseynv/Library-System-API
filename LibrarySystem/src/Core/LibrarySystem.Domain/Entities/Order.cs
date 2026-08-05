using LibrarySystem.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Order : BaseAccountable
    {
        public decimal TotalPrice { get; set; }
        //RelatedProperties
        public ICollection<OrderItem> OrderItems { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
