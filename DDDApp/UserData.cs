public class UserData
{
    public UserData(User source)
    {
        Id = source.Id.Value;
        Name = source.Name.Value;
        MailAddress = source.MailAddress.Value;
    }

    public string Id { get; }
    public string Name { get; }
    public string MailAddress { get; }
}