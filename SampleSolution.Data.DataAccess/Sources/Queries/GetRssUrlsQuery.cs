using MediatR;

namespace SampleSolution.Data.DataAccess.Sources.Queries;

public record GetRssUrlsQuery : IRequest<IReadOnlyCollection<Tuple<int, string>>>;