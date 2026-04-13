using MediatR;

namespace SampleSolution.Data.DataAccess.User.Commands;

public record InsertNewUserCommand : IRequest
{
    public required string Name { get; init; } 
    public required string Email { get; init; } 
    public required string PasswordHash { get; init; } 
    public required string Salt { get; init; } 
    public required int RoleId { get; init; } 
}