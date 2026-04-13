namespace SampleSolution.Core.DTOs;

public class UserCheckPasswordDto
{
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
}