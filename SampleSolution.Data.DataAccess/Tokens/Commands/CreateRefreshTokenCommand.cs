using MediatR;

namespace SampleSolution.Data.DataAccess.Tokens.Commands;

public record CreateRefreshTokenCommand(long UserId, Guid Token, string? DeviceName) : IRequest
{
}