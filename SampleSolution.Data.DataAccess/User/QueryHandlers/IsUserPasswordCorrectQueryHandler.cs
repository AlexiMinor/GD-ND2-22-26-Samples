using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.User.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.QueryHandlers;

public class IsUserPasswordCorrectQueryHandler(SampleDbContext dbContext) : IRequestHandler<IsUserPasswordCorrectQuery, bool>
{
    public async Task<bool> Handle(IsUserPasswordCorrectQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTrackingWithIdentityResolution()
            .AnyAsync(
                user => user.Email.Equals(request.Email) && user.PasswordHash.Equals(request.PasswordHash),
                cancellationToken);
    }
}