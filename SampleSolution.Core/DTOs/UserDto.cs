namespace SampleSolution.Core.DTOs;

public class UserDto
{
    public long UserId { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    //public string RegistrationDate { get; set; }
}