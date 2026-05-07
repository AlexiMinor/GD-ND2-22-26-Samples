using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticlesByRateAndSourceQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticlesByRateAndSourceQuery, IReadOnlyCollection<ArticleDto?>>
{
    public async Task<IReadOnlyCollection<ArticleDto?>> Handle(GetArticlesByRateAndSourceQuery request, CancellationToken cancellationToken)
    {
        var articles = dbContext.Articles.AsNoTrackingWithIdentityResolution()
            .AsQueryable();

        if (request.MinRate != null)
        {
            articles = articles.Where(a => a.Rate >= request.MinRate);
        }

        if (request.SourceId!= null)
        {
            articles = articles.Where(a => a.SourceId == request.SourceId);
        }

        return (await articles
            .Select(art=>ArticleMapper.ArticleToArticleDto(art))
            .ToArrayAsync(cancellationToken))
            .AsReadOnly();
    }
}