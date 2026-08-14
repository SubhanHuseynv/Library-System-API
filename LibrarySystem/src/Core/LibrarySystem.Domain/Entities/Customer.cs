using LibrarySystem.Domain.Entities.common;

namespace LibrarySystem.Domain.Entities;

public class Customer : BaseNameable
{
    //RelatedProperties
    public ICollection<Order> Orders { get; set; }
}
