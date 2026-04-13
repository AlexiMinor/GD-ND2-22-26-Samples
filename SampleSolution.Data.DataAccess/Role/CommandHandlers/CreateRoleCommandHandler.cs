using MediatR;
using SampleSolution.Data.DataAccess.Role.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Role.CommandHandlers;

public class CreateRoleCommandHandler(SampleDbContext dbContext) : IRequestHandler<CreateRoleCommand, int>
{
    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Roles.AddAsync(new Db.Entities.Role()
        {
            Name = request.RoleName
        }, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        // Here you can implement the logic to insert the parsed article into the database.
        return entry.Entity.Id;
    }
}