using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticleTextByIdQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticleTextByIdQuery, string?>
{
    public async Task<string?> Handle(GetArticleTextByIdQuery request, CancellationToken cancellationToken)
    {
        return (await dbContext.Articles
            .AsNoTracking()
            .SingleOrDefaultAsync(article => article.Id.Equals(request.Id), 
                cancellationToken))?.Text;

    }
}