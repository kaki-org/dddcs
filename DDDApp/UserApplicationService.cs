public class UserApplicationService
{
    private readonly IUserRepository userRepository;
    private readonly UserService userService;

    public UserApplicationService(IUserRepository userRepository, UserService userService)
    {
        this.userRepository = userRepository;
        this.userService = userService;
    }

    public void Register(string name)
    {
        var user = new User(
            new UserName(name)
        );
        if (userService.Exists(user))
        {
            throw new CanNotRegisterUserException(user, "ユーザは既に存在しています。");
        }

        userRepository.Save(user);
    }

    public UserData Get(string userId)
    {
        var targetId = new UserId(userId);
        var user = userRepository.Find(targetId);

        if (user == null)
        {
            return null;
        }

        return new UserData(user);
    }

    public void Update(UserUpdateCommand command)
    {
        var targetId = new UserId(command.Id);
        var user = userRepository.Find(targetId);

        if (user == null)
        {
            throw new UserNotFoundException(targetId);
        }

        var name = command.Name;
        // メールアドレスだけを更新するため、ユーザ名が指定されないことを考慮
        if (name != null)
        {
            var newUserName = new UserName(name);
            user.ChangeName(newUserName);
            if (userService.Exists(user))
            {
                throw new CanNotRegisterUserException(user, "ユーザは既に存在しています");
            }
        }

        var mailAddress = command.MailAddress;
        // メールアドレスを更新できるように
        if (mailAddress != null)
        {
            var newMailAddress = new MailAddress(mailAddress);
            user.ChangeMailAddress(newMailAddress);
        }

        userRepository.Save(user);
    }

    public void Delete(UserDeleteCommand command)
    {
        var targetId = new UserId(command.Id);
        var user = userRepository.Find(targetId);

        if (user == null)
        {
            throw new UserNotFoundException(targetId);
            // return null; // 対象がみつからなかったため退会成功とする
        }

        userRepository.Delete(user);
    }
    
}