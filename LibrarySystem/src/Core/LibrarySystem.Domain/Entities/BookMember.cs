namespace LibrarySystem.Domain.Entities;

public class BookMember
{
    public long BookId { get; set; }
    public Book Book { get; set; }
    public long MemberId { get; set; }
    public Member Member { get; set; }
}
