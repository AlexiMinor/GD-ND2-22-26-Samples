using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Sources.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Sources.QueryHandlers;

public class GetRssUrlsQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetRssUrlsQuery, IReadOnlyCollection<Tuple<int, string>>>
{
    public async Task<IReadOnlyCollection<Tuple<int, string>>> Handle(GetRssUrlsQuery request, CancellationToken cancellationToken)
    {
      return (await dbContext.Sources
                .Where(s => !string.IsNullOrWhiteSpace(s.RssUrl))
                .Select(source => new Tuple<int, string>(source.Id, source.RssUrl))
                .ToArrayAsync(cancellationToken))
            .AsReadOnly();

    }
}