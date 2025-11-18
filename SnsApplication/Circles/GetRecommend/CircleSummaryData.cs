using SnsDomain.Models.Circles;

namespace SnsApplication.Circles.GetRecommend
{
    public class CircleSummaryData
    {
        public CircleSummaryData(Circle circle)
        {
            Id = circle.Id.ToString();
            Name = circle.Name.ToString();
        }

        public string Id { get; }
        public string Name { get; }
    }
}
