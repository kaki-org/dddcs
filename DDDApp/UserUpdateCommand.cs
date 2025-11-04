public class UserUpdateCommand
{
    public UserUpdateCommand(string id, string name = null, string mailAddress = null)
    {
        Id = id;
        Name = name;
        MailAddress = mailAddress;
    }

    public string Id { get; }
    public string Name { get; } // セッターなし
    public string MailAddress { get; } // セッターなし

    
}