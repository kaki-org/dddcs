namespace SnsDomain.Models.Users
{
    public class User
    {
        // インスタンス変数はいずれも非公開
        public UserId Id { get; }
        public UserName Name { get; private set; }

        public User(UserId id, UserName name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
        }


        public void ChangeName(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
        }

    }
}
