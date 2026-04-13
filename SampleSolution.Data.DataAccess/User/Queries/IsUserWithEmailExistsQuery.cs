using MediatR;

namespace SampleSolution.Data.DataAccess.User.Queries;

public record IsUserWithEmailExistsQuery : IRequest<bool>
{
    public required string Email { get; init; }
}