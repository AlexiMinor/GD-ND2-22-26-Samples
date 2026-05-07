using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Commands;

public record InsertParsedArticlesCommand : IRequest<int>
{
    public IEnumerable<ArticleDto> Articles { get; init; } = [];
}