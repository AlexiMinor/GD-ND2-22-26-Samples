using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.Exceptions;
using SampleSolution.Data.DataAccess.Tokens.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Tokens.CommandHandlers;

public class RevokeRefreshTokenCommandHandler(SampleDbContext dbContext) : IRequestHandler<RevokeRefreshTokenCommand>
{

    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var rt = await dbContext.RefreshTokens.SingleOrDefaultAsync(rt => rt.Id == request.Token, cancellationToken);
        if (rt is null)
        {
            throw new NotFoundException($"Refresh token with id {request.Token} was not found.");
        }
        rt.IsRevoked = true;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}