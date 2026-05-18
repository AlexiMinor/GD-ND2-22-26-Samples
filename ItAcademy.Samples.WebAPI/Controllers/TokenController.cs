using ItAcademy.Samples.WebAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.Core.DTOs;
using SampleSolution.Core.Exceptions;
using SampleSolution.Data.Db.Entities;
using SampleSolution.TokenService;
using SampleSolution.UserService;

namespace ItAcademy.Samples.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(
        ITokenService tokenService,
        IUserService userService,
        ILogger<TokenController> logger)
        : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        [ProducesResponseType<TokenPairModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //login
        public async Task<IActionResult> GetAccessToken(LoginModel model,
            CancellationToken cancellationToken)
        {
            if (await userService.CheckUserExistsAndPasswordCorrectAsync(model.Username, model.Password, cancellationToken))
            {
                var userDto = await userService.GetUserByEmailAsync(model.Username, cancellationToken);
                if (userDto != null)
                {
                    var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                    var tokenPair = new TokenPairModel
                    {
                        AccessToken = tokenService.GenerateAccessToken(userDto),
                        RefreshToken = await tokenService.GenerateRefreshTokenAsync(userDto.UserId, clientIp, cancellationToken)
                    };
                    return Ok(tokenPair);
                }
                logger.LogWarning("ClaimsIdentity is null for user {Username}", model.Username);
                throw new InternalServerErrorException("ClaimsIdentity is null");
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType<TokenPairModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model,
            CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByRefreshTokenAsync(model.RefreshToken, cancellationToken);
            if (user is null)
            {
                return Unauthorized();
            }

            await tokenService.RemoveRefreshTokenAsync(model.RefreshToken, cancellationToken);
            var jwtToken = tokenService.GenerateAccessToken(user);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(user.UserId, 
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown", 
                cancellationToken);

            return Ok(new TokenPairModel
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost]
        [Route("revoke")]
        [ProducesResponseType<TokenPairModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevokeToken(RefreshTokenModel model,
            CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByRefreshTokenAsync(model.RefreshToken, cancellationToken);
            if (user is null)
            {
                return Unauthorized();
            }

            await tokenService.RevokeTokenAsync(model.RefreshToken, cancellationToken);
            

            return NoContent();
        }
    }
    
}
