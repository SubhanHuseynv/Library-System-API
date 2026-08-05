using LibrarySystem.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class OrderItem : BaseAccountable
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //RelatedProperties
        public long OrderId { get; set; }   
        public Order Order { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
    }
}
