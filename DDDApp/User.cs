public class User : IEquatable<User>
{
    public UserId Id { get; set; }
    public UserName Name { get; set; }

    public User(UserId id, string name)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));

        this.Id = id;
        ChangeUserName(id, name);
    }

    public void ChangeUserName(UserId id, string name)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (name.Length < 3) throw new ArgumentException("ユーザ名は3文字以上です",  nameof(name));

        this.Id = id;
        this.Name = new UserName(name);
    }

    public bool Equals(User other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Id, other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((User) obj);
    }
    public override string ToString()
    {
        return "Id: " +  Id.ToString() + ", Name: " + Name.ToString();
    }

    // 言語によりGetHashCodeの実装が不要な場合もある
    public override int GetHashCode()
    {
        return (Id != null ? Id.GetHashCode() : 0);
    }
}
