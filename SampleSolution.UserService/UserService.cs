using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Commands;
using SampleSolution.Data.DataAccess.User.Queries;

namespace SampleSolution.UserService;

public class UserService(ILogger<UserService> logger, IMediator mediator) : IUserService 
{
    public async Task RegisterUserAsync(RegisterUserDto userDto, CancellationToken token)
    {
        var salt = GetSalt();
        var passwordHash = await HashPasswordAsync(userDto.Password, salt, token);

        await mediator.Send(UserMapper.RegisterUserDtoToInsertCommand(userDto, passwordHash, salt), token);
    }

    public async Task<bool> CheckEmailAsync(string email, CancellationToken token)
    {
        return await mediator.Send(new IsUserWithEmailExistsQuery() { Email = email }, token);
    }

    public async Task<bool> CheckUserExistsAndPasswordCorrectAsync(string email, string modelPassword, CancellationToken token)
    {

        var userDto = await mediator.Send(new GetUserSaltAndPasswordHashByEmailQuery() { Email = email }, token);
        if (userDto == null)
        {
            logger.LogWarning("User with email {email} not exists", email);
            return false;
        }

        var passwordHash = await HashPasswordAsync(modelPassword, userDto.PasswordSalt, token);
        if (passwordHash.Equals(userDto.PasswordHash))
        {
            return true;
        }

        logger.LogWarning("Password for user with email {email} is incorrect", email);
        return false;

    }

    public async Task<ClaimsPrincipal?> GetLoginDataAsync(string email, CancellationToken token)
    {
        var userLoginData = await mediator.Send(new GetUserByEmailWithRoleQuery() { Email = email }, token);
        if (userLoginData != null)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userLoginData.Email),
                new Claim(ClaimTypes.Name, userLoginData.Name),
                new Claim(ClaimTypes.Role, userLoginData.RoleName),
                //new Claim("id", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }

        return null;

    }

    private string GetSalt()
    {
        return Guid.NewGuid().ToString("D");
    }

    private async Task<string> HashPasswordAsync(string password, string salt, CancellationToken token)
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes((password + salt).ToCharArray()));
        stream.Position = 0;

        var result = await MD5.HashDataAsync(stream, token);

        return Encoding.UTF8.GetString(result);
    }
}