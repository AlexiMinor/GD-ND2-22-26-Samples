using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

public class GetArticlesByPageQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetArticlesByPageQuery, ReadOnlyCollection<ArticlePreviewDto>>
{
    public async Task<ReadOnlyCollection<ArticlePreviewDto>> Handle(GetArticlesByPageQuery request, CancellationToken cancellationToken)
    {
        return (await dbContext.Articles
            .AsNoTracking()
            .OrderByDescending(article => article.PublishedDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectArticleToArticlePreviewDto()
            .ToArrayAsync(cancellationToken))
            .AsReadOnly();

    }
}