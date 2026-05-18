using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Core.Exceptions;
using SampleSolution.Data.DataAccess.User.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.QueryHandlers;

public class GetUserByRefreshTokenQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetUserByRefreshTokenQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens
            .AsNoTrackingWithIdentityResolution()
            .Include(refreshToken => refreshToken.User)
            .ThenInclude(user => user.Role)
            .SingleOrDefaultAsync(refreshToken
                    => refreshToken.Id == request.refreshToken,
                cancellationToken);

        if (token.IsExpired || token.IsRevoked)
        {
            throw new BadRequestException("Invalid refresh token");//create new unauthorized exception as a good solution
        }

        return UserMapper.UserEntityToUserDto(token.User);
    }
}