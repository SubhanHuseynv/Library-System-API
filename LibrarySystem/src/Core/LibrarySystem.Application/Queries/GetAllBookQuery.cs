namespace LibrarySystem.Application.Queries;

public class GetAllBookQuery
{
    public string? Filter { get; set; }
    public int ConSort { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
}
