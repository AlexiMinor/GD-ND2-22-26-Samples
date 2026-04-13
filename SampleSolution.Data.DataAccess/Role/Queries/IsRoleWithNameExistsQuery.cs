using MediatR;

namespace SampleSolution.Data.DataAccess.Role.Queries;

public record IsRoleWithNameExistsQuery(string RoleName) : IRequest<bool>;