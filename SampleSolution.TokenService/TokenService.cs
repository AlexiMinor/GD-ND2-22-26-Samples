using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SampleSolution.Core.DTOs;
using SampleSolution.Core.Exceptions;
using SampleSolution.Data.DataAccess.Tokens.Commands;
using SampleSolution.Data.Db.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleSolution.TokenService;

public class TokenService(IConfiguration configuration, ILogger<TokenService> logger, IMediator mediator)
    : ITokenService
{
    public string GenerateAccessToken(UserDto userDto)
    {
        try
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Email, userDto.Email),
                    new Claim("id", userDto.UserId.ToString()),
                    new Claim(ClaimTypes.Role, userDto.RoleName)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:TokenExpiryMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["Jwt:Audience"],
                Issuer = configuration["Jwt:Issuer"],
                IssuedAt = DateTime.UtcNow,
                //NotBefore = DateTime.UtcNow,
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while generating access token");
            throw new InternalServerErrorException("Error occurred while generating access token", e);
        }
    }

    public async Task<Guid> GenerateRefreshTokenAsync(long userId, string? deviceName = null, CancellationToken cancellationToken = default)
    {
        var refreshToken = Guid.NewGuid();
        await mediator.Send(new CreateRefreshTokenCommand(userId, refreshToken, deviceName), cancellationToken);
        
        return refreshToken;
    }

    public async Task RemoveRefreshTokenAsync(Guid refreshToken, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveRefreshTokenCommand(refreshToken), cancellationToken);
    }

    public async Task RevokeTokenAsync(Guid refreshToken, CancellationToken cancellationToken)
    {
        await mediator.Send(new RevokeRefreshTokenCommand(refreshToken), cancellationToken);
    }
}