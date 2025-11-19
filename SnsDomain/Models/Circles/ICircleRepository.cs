using Library.Specifications;

namespace SnsDomain.Models.Circles
{
    public interface ICircleRepository
    {
        public void Save(Circle circle);
        public Circle Find(CircleId id);
        public Circle Find(CircleName name);
        public List<Circle> Find(ISpecification<Circle> specification);
        public List<Circle> FindAll();
    }
}
