using LibrarySystem.Domain.Entities.common;

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
