using LibrarySystem.Domain.Entities.common;

namespace LibrarySystem.Domain.Entities
{
    public class Author : BaseNameable
    {
        //RelatedProperties
        public ICollection<Book> Books { get; set; }
    }
}
