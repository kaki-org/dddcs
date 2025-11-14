using SnsDomain.Models.Circles;
using SnsDomain.Models.Users;

namespace SnsDomain.Models.CircleInvitations
{
    public class CircleInvitation
    {
        public CircleInvitation(Circle circle, User fromUser, User invitedUser)
        {
            Circle = circle;
            FromUser = fromUser;
            InvitedUser = invitedUser;
        }

        public Circle Circle { get; }
        public User FromUser { get; }
        public User InvitedUser { get; }
    }
}
