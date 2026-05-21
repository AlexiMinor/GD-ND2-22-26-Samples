using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetExistedArticleUrlsQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetExistedArticleUrlsQuery, HashSet<string>>
{
    public async Task<HashSet<string>> Handle(GetExistedArticleUrlsQuery request, CancellationToken cancellationToken)
    {
        var uniqueUrls = await dbContext.Articles.Select(article => article.OriginalUrl)
            .Distinct()
            .ToArrayAsync(cancellationToken);

        return [.. uniqueUrls];
    }
}