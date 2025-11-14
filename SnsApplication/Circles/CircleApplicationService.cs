using System.Transactions;
using SnsApplication.Circles.Create;
using SnsApplication.Circles.Invite;
using SnsApplication.Circles.Join;
using SnsDomain.Models.CircleInvitations;
using SnsDomain.Models.Circles;
using SnsDomain.Models.Users;

namespace SnsApplication.Circles
{
    public class CircleApplicationService
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly CircleService circleService;
        private readonly IUserRepository userRepository;
        private readonly ICircleInvitationRepository circleInvitationRepository;

        public CircleApplicationService(
            ICircleFactory circleFactory,
            ICircleRepository circleRepository,
            CircleService circleService,
            IUserRepository userRepository
        )
        {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.circleService = circleService;
            this.userRepository = userRepository;
        }

        public void Create(CircleCreateCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var ownerId = new UserId(command.UserId);
                var owner = userRepository.Find(ownerId);
                if (owner == null)
                {
                    throw new UserNotFoundException(ownerId, "サークルのオーナーとなるユーザが見つかりませんでした。");
                }

                var name = new CircleName(command.Name);
                var circle = circleFactory.Create(name, owner);
                if (circleService.Exists(circle))
                {
                    throw new CanNotRegisterCircleException(circle, "サークルはすでに存在しています");
                }

                circleRepository.Save(circle);
                transaction.Complete();
            }
        }

        public void Join(CircleJoinCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var memberId = new UserId(command.UserId);
                var member = userRepository.Find(memberId);
                if (member == null)
                {
                    throw new UserNotFoundException(memberId, "ユーザが見つかりませんでした。");
                }

                var id = new CircleId(command.CircleId);
                var circle = circleRepository.Find(id);
                if (circle == null)
                {
                    throw new CircleNotFoundException(id, "サークルがみつかりませんでした");
                }

                // サークルのオーナー含めて30名か確認
                if (circle.Members.Count >= 29)
                {
                    throw new CircleFullException(id);
                }

                // メンバーを追加する
                circle.Members.Add(member);
                circleRepository.Save(circle);

                transaction.Complete();
            }
        }

        public void Invite(CircleInviteCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var fromUserId = new UserId(command.FromUserId);
                var fromUser = userRepository.Find(fromUserId);
                if (fromUser == null)
                {
                    throw new UserNotFoundException(fromUserId, "招待元ユーザが見つかりませんでした");
                }

                var invitedUserId = new UserId(command.InvitedUserId);
                var invitedUser = userRepository.Find(invitedUserId);
                if (invitedUser == null)
                {
                    throw new UserNotFoundException(invitedUserId, "招待先ユーザが見つかりませんでした");
                }

                var circleId = new CircleId(command.CircleId);
                var circle = circleRepository.Find(circleId);
                if (circle == null)
                {
                    throw new CircleNotFoundException(circleId, "サークルが見つかりませんでした");
                }

                // サークルのオーナーを含めて30名か確認
                if (circle.Members.Count >= 29)
                {
                    throw new CircleFullException(circleId);
                }

                var circleInvitation = new CircleInvitation(circle, fromUser, invitedUser);
                circleInvitationRepository.Save(circleInvitation);
                transaction.Complete();

            }
        }
    }
}
