using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Role.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Role.QueryHandlers;

public class IsRoleWithNameExistsQueryHandler(SampleDbContext context) : IRequestHandler<IsRoleWithNameExistsQuery, bool>
{
    public async Task<bool> Handle(IsRoleWithNameExistsQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles.AnyAsync(r => r.Name == request.RoleName, cancellationToken);
    }
}