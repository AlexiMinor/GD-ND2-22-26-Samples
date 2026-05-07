using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetArticlesByRateAndSourceQuery(decimal? MinRate, int? SourceId) : IRequest<IReadOnlyCollection<ArticleDto?>>;