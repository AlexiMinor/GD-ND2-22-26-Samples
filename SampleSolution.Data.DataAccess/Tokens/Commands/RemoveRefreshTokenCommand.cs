using MediatR;

namespace SampleSolution.Data.DataAccess.Tokens.Commands;

public record RemoveRefreshTokenCommand(Guid Token) : IRequest;