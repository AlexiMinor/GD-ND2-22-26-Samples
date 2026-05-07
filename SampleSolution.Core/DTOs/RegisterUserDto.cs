namespace SampleSolution.Core.DTOs;

public class RegisterUserDto
{
    public string Name { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
}