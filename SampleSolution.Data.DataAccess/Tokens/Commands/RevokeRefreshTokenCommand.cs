using MediatR;

namespace SampleSolution.Data.DataAccess.Tokens.Commands;

public record RevokeRefreshTokenCommand(Guid Token) : IRequest;