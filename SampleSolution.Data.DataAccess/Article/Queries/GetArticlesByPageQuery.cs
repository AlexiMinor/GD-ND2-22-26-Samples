using System.Collections.ObjectModel;
using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetArticlesByPageQuery : IRequest<ReadOnlyCollection<ArticlePreviewDto>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}