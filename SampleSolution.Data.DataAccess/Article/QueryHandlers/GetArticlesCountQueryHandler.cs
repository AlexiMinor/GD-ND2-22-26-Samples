using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticlesCountQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticlesCountQuery, int>
{
    public async Task<int> Handle(GetArticlesCountQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Articles.CountAsync(cancellationToken);
    }
}