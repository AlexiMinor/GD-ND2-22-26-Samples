using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetArticlesByRateAndPageQuery(int? MinRate, int PageNumber, int PageSize) : IRequest<IReadOnlyCollection<ArticleDto?>>;