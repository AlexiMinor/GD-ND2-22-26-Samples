using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetUnratedArticlesQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetUnratedArticlesQuery, ArticleDto?[]>
{
    public async Task<ArticleDto?[]> Handle(GetUnratedArticlesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Articles
            .AsNoTracking()
            .Where(article => article.Rate == null)
            .Select(article => ArticleMapper.ArticleToArticleDto(article))
            .ToArrayAsync(cancellationToken);
    }
}