namespace User.Application.Exceptions;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string name, Object key):base($"Entity {name} - {key} is not found.")
    {
        
    }
}
