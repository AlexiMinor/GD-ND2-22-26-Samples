using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.User.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.User.CommandHandlers;

public class InsertNewUserCommandHandler(SampleDbContext dbContext) : IRequestHandler<InsertNewUserCommand>
{
    public async Task Handle(InsertNewUserCommand request, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(UserMapper.InsertUserCommandToUser(request), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}