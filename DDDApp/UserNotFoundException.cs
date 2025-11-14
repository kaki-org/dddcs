using System;

public class UserNotFoundException : Exception
{
    public UserId UserId { get; }

    public UserNotFoundException(UserId userId)
    {
        UserId = userId;
    }
}