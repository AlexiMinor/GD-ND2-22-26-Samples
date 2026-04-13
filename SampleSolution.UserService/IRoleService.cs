using MediatR;
using SampleSolution.Data.DataAccess.Role.Commands;
using SampleSolution.Data.DataAccess.Role.Queries;

namespace SampleSolution.UserService;

public interface IRoleService
{
    public Task<bool> CheckRoleExistsAsync(string roleName, CancellationToken cancellationToken);

    /// <summary>
    /// Create role 
    /// </summary>
    /// <param name="roleName">roleName</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>id of created role</returns>
    public Task<int> AddRoleAsync(string roleName, CancellationToken cancellationToken);

    public Task DeleteRoleAsync(string roleName, CancellationToken cancellationToken);
    Task<int> GetRoleIdAsync(string roleName, CancellationToken token);
}

public class RoleService(IMediator mediator) : IRoleService
{
    public async Task<bool> CheckRoleExistsAsync(string roleName, CancellationToken cancellationToken)
    {
        var isRoleExists = await mediator.Send(new IsRoleWithNameExistsQuery(roleName), cancellationToken);
        return isRoleExists;
    }
    public async Task<int> AddRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateRoleCommand(roleName), cancellationToken);
    }
    public Task DeleteRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();//todo
    }

    public async Task<int> GetRoleIdAsync(string roleName, CancellationToken token)
    {
        return await mediator.Send(new GetRoleIdByNameQuery(roleName), token);

    }
}