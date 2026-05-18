using MediatR;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetArticleTextByIdQuery(long Id) :  IRequest<string?>;