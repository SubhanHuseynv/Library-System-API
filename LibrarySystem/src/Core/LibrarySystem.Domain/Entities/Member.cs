using LibrarySystem.Domain.Entities.common;

namespace LibrarySystem.Domain.Entities;

public class Member : BaseNameable
{
    //RelatedProperties
    public ICollection<BookMember> BookMembers { get; set; }
}
