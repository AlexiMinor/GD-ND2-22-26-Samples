using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.QueryHandlers;

public class GetUserByEmailQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetUserByEmailQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return UserMapper.UserEntityToUserDto(await dbContext.Users
            .AsNoTrackingWithIdentityResolution()
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken));
    }
}