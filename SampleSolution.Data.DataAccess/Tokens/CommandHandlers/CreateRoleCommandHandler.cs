using MediatR;
using Microsoft.Extensions.Configuration;
using SampleSolution.Data.DataAccess.Tokens.Commands;
using SampleSolution.Data.Db;
using SampleSolution.Data.Db.Entities;

namespace SampleSolution.Data.DataAccess.Tokens.CommandHandlers;

public class CreateRefreshTokenCommandHandler(SampleDbContext dbContext, IConfiguration configuration) : IRequestHandler<CreateRefreshTokenCommand>
{

    public async Task Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            UserId = request.UserId,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToInt32(configuration["Jwt:RefreshTokenExpiryDays"])), // Set the expiry date as needed
            Device = request.DeviceName,
            IsRevoked = false,
            Id = request.Token
        };
        await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}