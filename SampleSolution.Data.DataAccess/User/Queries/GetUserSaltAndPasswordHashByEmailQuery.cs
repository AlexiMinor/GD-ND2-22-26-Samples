using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.User.Queries;

public class GetUserSaltAndPasswordHashByEmailQuery : IRequest<UserCheckPasswordDto?>
{
    public required string Email { get; set; }
}