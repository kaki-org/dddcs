namespace SnsDomain.Models.CircleMembers
{
    public class CircleMembersFullSpecification
    {
        // CircleFullSpecificationと違い、リポジトリは利用しない
        // private readonly IUserRepository userRepository;
        //
        // public CircleMembersFullSpecification(IUserRepository userRepository)
        // {
        //     this.userRepository = userRepository;
        // }
        public bool IsSatisfiedBy(CircleMembers members)
        {
            var premiumUserNumber = members.CountPremiumMembers(false);
            // サークルに所属しているプレミアムユーザの人数により上限が変わる
            var circleUpperLimit = premiumUserNumber < 10 ? 30 : 50;
            return members.CountMembers() >= circleUpperLimit;
        }
    }
}
