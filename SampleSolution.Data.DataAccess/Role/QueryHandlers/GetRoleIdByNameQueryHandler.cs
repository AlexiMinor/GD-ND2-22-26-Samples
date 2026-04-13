using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Role.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Role.QueryHandlers;

public class GetRoleIdByNameQueryHandler(SampleDbContext context) : IRequestHandler<GetRoleIdByNameQuery, int>
{
    public async Task<int> Handle(GetRoleIdByNameQuery request, CancellationToken cancellationToken)
    {
        return (await context.Roles.SingleAsync(r => r.Name == request.RoleName, cancellationToken)).Id;
    }
}