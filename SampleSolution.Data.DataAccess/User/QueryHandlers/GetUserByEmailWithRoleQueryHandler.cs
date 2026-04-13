using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.QueryHandlers;

public class GetUserByEmailWithRoleQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetUserByEmailWithRoleQuery, UserLoginDto?>
{
    public async Task<UserLoginDto?> Handle(GetUserByEmailWithRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTrackingWithIdentityResolution()
            .Include(user => user.Role)
            .Select(us => new UserLoginDto
            {
                Name = us.Name,
                Email = us.Email,
                RoleName = us.Role.Name
            })
            .SingleOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken);

        return user;
    }
}