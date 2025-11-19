using System.Transactions;
using SnsApplication.Circles.Create;
using SnsApplication.Circles.GetRecommend;
using SnsApplication.Circles.GetSummaries;
using SnsApplication.Circles.Invite;
using SnsApplication.Circles.Join;
using SnsApplication.Circles.Update;
using SnsDomain.Models.CircleInvitations;
using SnsDomain.Models.CircleMembers;
using SnsDomain.Models.Circles;
using SnsDomain.Models.Users;
using CircleSummaryData = SnsApplication.Circles.GetSummaries.CircleSummaryData;

namespace SnsApplication.Circles
{
    public class CircleApplicationService
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly CircleService circleService;
        private readonly IUserRepository userRepository;
        private readonly ICircleInvitationRepository circleInvitationRepository;
        private readonly DateTime now;

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
                var circleId = new CircleId(command.CircleId);
                var circle = circleRepository.Find(circleId);
                if (circle == null)
                {
                    throw new CircleNotFoundException(circleId, "サークルがみつかりませんでした");
                }

                // ファーストクラスコレクションに詰め替える処理
                var owner = userRepository.Find(circle.Owner);
                var members = userRepository.Find(circle.Members);
                var circleMembers = new CircleMembers(circle.Id, owner, members);
                var circleFullSpec = new CircleMembersFullSpecification();

                if (circleFullSpec.IsSatisfiedBy(circleMembers))
                {
                    throw new CircleFullException(circleId);
                }

                var memberId = new UserId(command.UserId);
                var member = userRepository.Find(memberId);
                if (member == null)
                {
                    throw new UserNotFoundException(memberId, "ユーザが見つかりませんでした。");
                }

                // メンバーを追加する
                circle.Join(member);
                circleRepository.Save(circle);

                transaction.Complete();
            }
        }

        public void Update(CircleUpdateCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var id = new CircleId(command.Id);
                // この時点でUserのインスタンスが再構築されるが
                var circle = circleRepository.Find(id);
                if (circle == null)
                {
                    throw new CircleNotFoundException(id);
                }

                if (command.Name != null)
                {
                    var name = new CircleName(command.Name);
                    circle.ChangeName(name);
                    if (circleService.Exists(circle))
                    {
                        throw new CanNotRegisterCircleException(circle, "サークルはすでに存在しています");
                    }
                }

                circleRepository.Save(circle);

                transaction.Complete();
                // Userのインスタンスは使われることなく捨てられる
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

                // サークルのオーナーを含めて上限値かどうかを確認
                if (circle.IsFull())
                {
                    throw new CircleFullException(circleId);
                }

                var circleInvitation = new CircleInvitation(circle, fromUser, invitedUser);
                circleInvitationRepository.Save(circleInvitation);
                transaction.Complete();

            }
        }

        public CircleGetRecommendResult GetRecommend(CircleGetRecommendRequest request)
        {
            var circleRecommendSpecification = new CircleRecommendSpecification(now);
            // リポジトリに仕様を引き渡して抽出(フィルタリング)
            var recommendCircles = circleRepository.Find(circleRecommendSpecification)
                .Take(10)
                .ToList();

            return new CircleGetRecommendResult(recommendCircles);
        }

        public CircleGetSummariesResult GetSummaries(CircleGetSummariesCommand command)
        {
            // 全件取得して
            var all = circleRepository.FindAll();
            // その後にページング
            var circles = all
                .Skip((command.Page - 1) * command.Size)
                .Take(command.Size);

            var summaries = new List<CircleSummaryData>();
            foreach (var circle in circles)
            {
                // サークルのオーナーを改めて検索
                var owner = userRepository.Find(circle.Owner);
                summaries.Add(new CircleSummaryData(circle.Id.Value, owner.Name.Value));
            }

            return new CircleGetSummariesResult(summaries);
        }
    }
}
