using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Commands;

public record UpdateArticlePartiallyCommand(long Id, string? UpdatedModelTitle, decimal? UpdatedModelRate) : IRequest
{
}