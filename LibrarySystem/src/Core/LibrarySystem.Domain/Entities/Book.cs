using LibrarySystem.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Book : BaseNameable
    {
        public int TotalCount { get; set; }
        public string Description { get; set; }
        //RelatedProperties
        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
