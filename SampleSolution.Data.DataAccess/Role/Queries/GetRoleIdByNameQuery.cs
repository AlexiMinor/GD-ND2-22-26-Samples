using MediatR;

namespace SampleSolution.Data.DataAccess.Role.Queries;

public record GetRoleIdByNameQuery(string RoleName) : IRequest<int>;