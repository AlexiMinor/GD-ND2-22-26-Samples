using SampleSolution.Core.DTOs;
using System.Security.Claims;

namespace SampleSolution.UserService
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterUserDto userDto, CancellationToken token);

        Task<bool> CheckEmailAsync(string email, CancellationToken token);

        Task<bool> CheckUserExistsAndPasswordCorrectAsync(string email, string modelPassword,
            CancellationToken token);

        Task<ClaimsPrincipal?> GetLoginDataAsync(string email, CancellationToken token);
    }
}
