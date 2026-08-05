using LibrarySystem.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Customer : BaseNameable
    {
        //RelatedProperties
        public ICollection<Order> Orders { get; set; }
    }
}
