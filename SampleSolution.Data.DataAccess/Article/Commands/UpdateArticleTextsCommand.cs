using MediatR;

namespace SampleSolution.Data.DataAccess.Article.Commands;

public record UpdateArticleTextsCommand(Dictionary<long, string> ArticleTexts) : IRequest;