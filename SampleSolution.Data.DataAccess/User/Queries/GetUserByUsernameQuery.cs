using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.User.Queries;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto?>;