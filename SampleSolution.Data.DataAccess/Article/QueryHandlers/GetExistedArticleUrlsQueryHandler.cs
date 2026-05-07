using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetExistedArticleUrlsQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetExistedArticleUrlsQuery, ReadOnlyCollection<string>>
{
    public async Task<ReadOnlyCollection<string>> Handle(GetExistedArticleUrlsQuery request, CancellationToken cancellationToken)
    {
        return (await dbContext.Articles.Select(article => article.OriginalUrl)
                .ToArrayAsync(cancellationToken))
            .AsReadOnly();

    }
}