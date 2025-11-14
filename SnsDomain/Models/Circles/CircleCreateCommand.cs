namespace SnsDomain.Models.Circles
{
    public class CircleCreateCommand(string userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    
    public string UserId { get; }
    public string Name { get; }
}
