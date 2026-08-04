using LibrarySystem.Domain.Entities.common;

namespace LibrarySystem.Domain.Entities
{
    public class Category : BaseNameable
    {
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
