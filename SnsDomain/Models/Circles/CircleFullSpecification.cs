using SnsDomain.Models.Users;

namespace SnsDomain.Models.Circles
{
    public class CircleFullSpecification
    {
        private readonly IUserRepository userRepository;

        public CircleFullSpecification(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool IsSatisfiedBy(Circle circle)
        {
            var users = userRepository.Find(circle.Members);
            var premiumUserNumber = users.Count(user => user.IsPremium);
            // サークルに所属しているプレミアムユーザの人数により上限が変わる
            var circleUpperLimit = premiumUserNumber < 10 ? 30 : 50;
            return circle.CountMembers() >= circleUpperLimit;
        }
    }
}
