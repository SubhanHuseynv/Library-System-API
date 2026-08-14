namespace LibrarySystem.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base() { }    
        public ConflictException(string name) : base($"{name} name is already exists") { }
        
    }
}
