using MediatR;

namespace SampleSolution.Data.DataAccess.User.Queries;

public record IsUserPasswordCorrectQuery : IRequest<bool>
{
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
}