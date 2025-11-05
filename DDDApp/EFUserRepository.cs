using System.Linq;

public class EFUserRepository : IUserRepository
{
    private readonly MyDbContext context;

    public EFUserRepository(MyDbContext context)
    {
        this.context = context;
    }

    public User Find(UserName name)
    {
        var target = context.Users.FirstOrDefault(userData => userData.Name == name.Value);
        if (target == null)
        {
            return null;
        }

        return ToModel(target);
    }

    public User Find(UserId id)
    {
        var target = context.Users.FirstOrDefault(userData => userData.Id == id.Value);
        if (target == null)
        {
            return null;
        }
        return ToModel(target);
    }

    public User Find(MailAddress mail)
    {
        var target = context.Users.FirstOrDefault(userData => userData.MailAddress == mail.Value);
        if (target == null)
        {
            return null;
        }
        return ToModel(target);
    }

    public bool Exists(UserName name)
    {
        return context.Users.Any(userData => userData.Name == name.Value);
    }

    public void Save(User user)
    {
        var found = context.Users.Find(user.Id.Value);

        if (found == null)
        {
            var data = ToDataModel(user);
            context.Users.Add(data);
        }
        else
        {
            var data = Transfer(user, found);
            context.Users.Update(data);
        }

        context.SaveChanges();
    }

    public void Delete(User user)
    {
        var found = context.Users.Find(user.Id.Value);
        if (found != null)
        {
            context.Users.Remove(found);
            context.SaveChanges();
        }
    }

    private User ToModel(UserDataModel from)
    {
        return new User(
            new UserId(from.Id),
            new UserName(from.Name),
            new MailAddress(from.MailAddress)
        );
    }

    private UserDataModel Transfer(User from, UserDataModel model)
    {
        model.Id = from.Id.Value;
        model.Name = from.Name.Value;
        model.MailAddress = from.MailAddress.Value;

        return model;
    }

    private UserDataModel ToDataModel(User from)
    {
        return new UserDataModel
        {
            Id = from.Id.Value,
            Name = from.Name.Value,
            MailAddress = from.MailAddress.Value,
        };
    }
    
}