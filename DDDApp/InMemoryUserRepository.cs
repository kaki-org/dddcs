public class InMemoryUserRepository : IUserRepository
{
    // テストケースによってはデータを確認したいことがある
    // 確認のための操作を外部から行えるようにするためpublicにしている
    public Dictionary<UserId, User> Store { get; } = new Dictionary<UserId, User>();

    public User Find(UserName userName)
    {
        var target = Store.Values.FirstOrDefault(user => userName.Equals(user.Name));

        if (target != null)
        {
            // インスタンスを直接返さずディープコピーを行う
            return Clone(target);
        }
        else
        {
            return null;
        }
    }

    public User Find(UserId userId)
    {
        var target = Store.Values.FirstOrDefault(user => user.Id.Equals(userId));
        if (target != null)
        {
            return Clone(target);
        }
        else
        {
            return null;
        }
    }

    public User Find(MailAddress mailAddress)
    {
        var target = Store.Values.FirstOrDefault(user => user.MailAddress.Equals(mailAddress));
        if (target != null)
        {
            return Clone(target);
        }
        else
        {
            return null;
        }
    }

    public void Save(User user)
    {
        // 保存時もディープコピーを行う
        Store[user.Id] = Clone(user);
    }

    public void Delete(User user)
    {
        Store.Remove(user.Id);
    }

    // ディープコピーを行うメソッド
    private User Clone(User user)
    {
        return new User(user.Id, user.Name, user.MailAddress);
    }

}