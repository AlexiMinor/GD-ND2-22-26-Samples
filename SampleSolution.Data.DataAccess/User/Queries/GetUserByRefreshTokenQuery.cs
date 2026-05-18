using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.User.Queries;

public record GetUserByRefreshTokenQuery(Guid refreshToken) : IRequest<UserDto?>;