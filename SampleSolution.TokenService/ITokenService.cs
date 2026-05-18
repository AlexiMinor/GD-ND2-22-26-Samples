using System.Security.Claims;
using SampleSolution.Core.DTOs;

namespace SampleSolution.TokenService
{
    public interface ITokenService
    {
        public string GenerateAccessToken(UserDto userDto);
        public Task<Guid> GenerateRefreshTokenAsync (long userId, string? deviceName = null, CancellationToken cancellationToken = default);
        Task RemoveRefreshTokenAsync(Guid refreshToken, CancellationToken cancellationToken);
        Task RevokeTokenAsync(Guid refreshToken, CancellationToken cancellationToken);
    }
}
