using MediatR;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetArticlesCountQuery(int? MinRate) :  IRequest<int>;