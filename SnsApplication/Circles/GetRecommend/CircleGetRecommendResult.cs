using SnsDomain.Models.Circles;

namespace SnsApplication.Circles.GetRecommend
{
    public class CircleGetRecommendResult
    {
        public CircleGetRecommendResult(List<Circle> recommendCircles)
        {
            recommendCircles.Select(x => new CircleSummaryData(x));
        }

        public List<CircleSummaryData> Summaries { get; }

    }
}
