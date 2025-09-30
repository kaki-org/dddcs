public class User
{
    public UserId Id { get; set; }
    public UserName Name { get; set; }

    public User(string name)
    {
        ChangeName(name);
    }

    public void ChangeName(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (name.Length < 3) throw new ArgumentException("ユーザ名は3文字以上です",  nameof(name));
        
        this.Name = new UserName(name);
    }
    public override string ToString()
    {
        return "Id: " +  Id.ToString() + ", Name: " + Name.ToString();
    }
}
