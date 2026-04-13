using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Commands;

public record InsertParsedArticlesCommand : IRequest
{
    public IEnumerable<ArticleDto> Articles { get; init; } = [];
}