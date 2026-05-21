using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticlesWithoutTextQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticlesWithoutTextQuery, ArticleDto?[]>
{
    public async Task<ArticleDto?[]> Handle(GetArticlesWithoutTextQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Articles
            .AsNoTracking()
            .Where(article => string.IsNullOrEmpty(article.Text))
            .Select(article => ArticleMapper.ArticleToArticleDto(article))
            .ToArrayAsync(cancellationToken);
    }
}