public interface IUserRepository
{
    User Find(UserId id);
    User Find(UserName name);
    User Find(MailAddress mail);
    void Save(User user);
    void Delete(User user);
}