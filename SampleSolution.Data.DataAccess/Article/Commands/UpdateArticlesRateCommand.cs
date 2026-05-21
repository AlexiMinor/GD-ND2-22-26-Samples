using MediatR;

namespace SampleSolution.Data.DataAccess.Article.Commands;

public record UpdateArticleRatesCommand(Dictionary<long, decimal> ArticleRates) : IRequest;