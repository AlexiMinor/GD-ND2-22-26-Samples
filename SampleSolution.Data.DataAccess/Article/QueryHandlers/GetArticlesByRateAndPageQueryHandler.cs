using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticlesByRateAndPageQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticlesByRateAndPageQuery, IReadOnlyCollection<ArticleDto?>>
{
    public async Task<IReadOnlyCollection<ArticleDto?>> Handle(GetArticlesByRateAndPageQuery request, CancellationToken cancellationToken)
    {
        var articles = dbContext.Articles.AsNoTrackingWithIdentityResolution()
            .AsQueryable();

        if (request.MinRate.HasValue)
        {
            articles = articles.Where(a => a.Rate >= request.MinRate);
        }
        
        articles = articles
            .OrderByDescending(article => article.PublishedDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
                           .Take(request.PageSize);

        return (await articles
            .Select(art=>ArticleMapper.ArticleToArticleDto(art))
            .ToArrayAsync(cancellationToken))
            .AsReadOnly();
    }
}