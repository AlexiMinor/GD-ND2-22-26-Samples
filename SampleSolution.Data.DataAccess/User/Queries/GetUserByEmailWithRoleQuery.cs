using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.User.Queries;

public class GetUserByEmailWithRoleQuery : IRequest<UserLoginDto?>
{
    public required string Email { get; set; }
}