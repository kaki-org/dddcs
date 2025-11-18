using System.Dynamic;
using Library.Specifications;
using Microsoft.Data.SqlClient;
using SnsDomain.Models.Circles;
using SnsDomain.Models.Users;

namespace SqlInfrastructure.Persistence
{
    public class CircleRepository : ICircleRepository
    {
        private readonly SqlConnection connection;

        public CircleRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Save(Circle circle)
        {
            throw new System.NotImplementedException();
        }
        public Circle Find(CircleId id)
        {
            throw new System.NotImplementedException();
        }
        public Circle Find(CircleName name)
        {
            throw new System.NotImplementedException();
        }

        public List<Circle> Find(ISpecification<Circle> specification)
        {
            using (var command = connection.CreateCommand())
            {
                // 全件取得するクエリを発行
                command.CommandText = "SELECT * FROM circles";
                using (var reader = command.ExecuteReader())
                {
                    var circles = new List<Circle>();
                    while (reader.Read())
                    {
                        // インスタンスを生成して条件に合うか確認している(合わなければ捨てられる)
                        var circle = CreateInstance(reader);
                        if (specification.IsSatisfiedBy(circle))
                        {
                            circles.Add(circle);
                        }
                    }
                    return circles;
                }
            }
        }

        private Circle CreateInstance(SqlDataReader reader)
        {
            return new Circle(
                new CircleId((string)reader["id"]),
                new CircleName((string)reader["name"]),
                new UserId((string)reader["owner"]),
                new List<UserId>()
            );
        }
    }

}
