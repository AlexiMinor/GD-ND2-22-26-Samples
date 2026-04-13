using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.User.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.QueryHandlers;

public class IsUserWithEmailExistsQueryHandler(SampleDbContext dbContext) : IRequestHandler<IsUserWithEmailExistsQuery, bool>
{
    public async Task<bool> Handle(IsUserWithEmailExistsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AsNoTrackingWithIdentityResolution()
            .AnyAsync(u => u.Email.Equals(request.Email), cancellationToken);
    }
}