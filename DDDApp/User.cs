public class User
{
    public User(UserId id, UserName name)
    {
        if (id== null)  throw new ArgumentNullException(nameof(id));
        if (name == null)  throw new ArgumentNullException(nameof(name));

        // Id = new UserId(Guid.NewGuid().ToString());
        Id = id;
        Name = name;
    }

    public UserId Id { get; }
    public UserName Name { get; }
}
