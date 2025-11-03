using System;

public class CanNotRegisterUserException : Exception
{
    public User User { get; }

    public CanNotRegisterUserException(User user, string message) : base(message)
    {
        User = user;
    }

    public CanNotRegisterUserException(User user, string message, Exception innerException) : base(message, innerException)
    {
        User = user;
    }
}