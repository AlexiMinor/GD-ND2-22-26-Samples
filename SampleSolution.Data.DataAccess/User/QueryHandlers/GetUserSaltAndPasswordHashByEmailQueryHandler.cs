using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.QueryHandlers;

public class GetUserSaltAndPasswordHashByEmailQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetUserSaltAndPasswordHashByEmailQuery, UserCheckPasswordDto?>
{
    public async Task<UserCheckPasswordDto?> Handle(GetUserSaltAndPasswordHashByEmailQuery request, CancellationToken cancellationToken)
    {
        return UserMapper.UserEntityToUserCheckPasswordDto(await dbContext.Users.AsNoTrackingWithIdentityResolution()
            .SingleOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken));
    }
}