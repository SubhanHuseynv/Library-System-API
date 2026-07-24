namespace LibrarySystem.Application.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(): base("Authentication required. Please log in.") { }

    public UnauthorizedException(string message): base(message) { }
}
