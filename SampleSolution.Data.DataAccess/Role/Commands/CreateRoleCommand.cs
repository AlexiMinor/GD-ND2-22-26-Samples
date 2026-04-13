using MediatR;

namespace SampleSolution.Data.DataAccess.Role.Commands;

public record CreateRoleCommand(string RoleName) : IRequest<int>;
