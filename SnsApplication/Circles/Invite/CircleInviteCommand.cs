namespace SnsApplication.Circles.Invite
{
    public class CircleInviteCommand
    {
        public string CircleId { get; set; }
        public string FromUserId { get; set; }
        public string InviteUserId { get; set; }

        public CircleInviteCommand(string circleId, string fromUserId, string inviteUserId)
        {
            CircleId = circleId;
            FromUserId = fromUserId;
            InviteUserId = inviteUserId;
        }
    }
}
