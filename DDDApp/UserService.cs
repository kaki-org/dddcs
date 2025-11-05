public class UserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public bool Exists(User user)
    {
        // ユーザ名により重複確認を行うという知識は失われている
        // var duplicatedUser = userRepository.Find(user.Name);

        // 重複のルールをユーザ名からメールアドレスに変更
        var duplicatedUser = userRepository.Find(user.MailAddress);
        
        return duplicatedUser != null;
    }
    
}