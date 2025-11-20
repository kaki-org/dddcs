using EFInfrastructure.Contexts;
using EFInfrastructure.Services.Circles.GetSummaries;

namespace EFInfrastructure.Services.Circles
{
    public class EFCircleQueryService
    {
        private readonly MyDbContext context;

        public EFCircleQueryService(MyDbContext context)
        {
            this.context = context;
        }

        public CircleGetSummariesResult GetSummaries(CircleGetSummariesCommand command)
        {
            var all =
                from circle in context.Circles
                join owner in context.Users
                    on circle.OwnerId equals owner.Id
                select new { circle, owner };

            var page = command.Page;
            var size = command.Size;

            var chunk = all
                .Skip((page - 1) * size)
                .Take(size);

            var summaries = chunk
                .Select(x => new CircleSummaryData(x.circle.Id, x.owner.Name))
                .ToList();

            return new CircleGetSummariesResult(summaries);
        }

    }

}
