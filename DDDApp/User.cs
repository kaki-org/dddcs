public class User
{
    public UserId Id { get; set; }
    public UserName Name { get; set; }

    public override string ToString()
    {
        return "Id: " +  Id.ToString() + ", Name: " + Name.ToString();
    }
}
