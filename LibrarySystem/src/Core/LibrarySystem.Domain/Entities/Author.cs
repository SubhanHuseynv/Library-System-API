using LibrarySystem.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Author : BaseNameable
    {
        //RelatedProperties
        public ICollection<Book> Books { get; set; }
    }
}
