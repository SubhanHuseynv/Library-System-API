using LibrarySystem.Domain.Entities.common;

namespace LibrarySystem.Domain.Entities
{
    public class Book : BaseNameable
    {
        public int TotalCount { get; set; }
        public string Description { get; set; }
        //RelatedProperties
        public ICollection<BookMember> BookMembers { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
