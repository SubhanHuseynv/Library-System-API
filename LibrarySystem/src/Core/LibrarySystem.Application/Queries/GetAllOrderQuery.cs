namespace LibrarySystem.Application.Queries;

public class GetAllOrderQuery
{
    public long CustomerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

}
