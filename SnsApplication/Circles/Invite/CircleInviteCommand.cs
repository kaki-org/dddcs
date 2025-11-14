namespace SnsApplication.Circles.Invite
{
    public class CircleInviteCommand
    {
        public string CircleId { get; set; }
        public string FromUserId { get; set; }
        public string InvitedUserId { get; set; }

        public CircleInviteCommand(string circleId, string fromUserId, string invitedUserId)
        {
            CircleId = circleId;
            FromUserId = fromUserId;
            InvitedUserId = invitedUserId;
        }
    }
}
