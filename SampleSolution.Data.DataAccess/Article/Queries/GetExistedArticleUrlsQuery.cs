using System.Collections.ObjectModel;
using MediatR;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetExistedArticleUrlsQuery : IRequest<ReadOnlyCollection<string>>;
