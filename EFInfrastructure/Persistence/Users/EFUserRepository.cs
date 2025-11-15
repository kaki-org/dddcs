using EFInfrastructure.Contexts;
using EFInfrastructure.Persistence.DataModels;
using SnsDomain.Models.Users;

namespace EFInfrastructure.Persistence.Users
{
    public class EFUserRepository
    {
        private readonly MyDbContext context;

        public EFUserRepository(MyDbContext context)
        {
            this.context = context;
        }

        public User Find(UserId id)
        {
            throw new NotImplementedException();
        }

        public User Find(UserName name)
        {
            throw new NotImplementedException();
        }

        public List<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Save(User user)
        {
            // // ゲッターを利用しデータの詰め替えをしている
            // var userDataModel = new UserDataModel
            // {
            //     Id = user.Id.Value,
            //     Name = user.Name.Value
            // };

            // 通知オブジェクトを引き渡して内部データを取得
            var userDataModelBuilder = new UserDataModelBuilder();
            user.Notify(userDataModelBuilder);

            // 通知された内部データからデータモデルを生成
            var userDataModel = userDataModelBuilder.Build();

            context.Users.Add(userDataModel);
            context.SaveChanges();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}
