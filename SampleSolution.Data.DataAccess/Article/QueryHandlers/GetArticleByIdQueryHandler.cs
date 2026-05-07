using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticleByIdQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticleByIdQuery, ArticleDto?>
{
    public async Task<ArticleDto?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return ArticleMapper.ArticleToArticleDto(await dbContext.Articles
            .AsNoTracking()
            .SingleOrDefaultAsync(article => article.Id.Equals(request.Id), 
                cancellationToken));

    }
}