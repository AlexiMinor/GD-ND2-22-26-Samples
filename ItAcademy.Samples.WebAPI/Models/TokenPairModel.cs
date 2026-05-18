namespace ItAcademy.Samples.WebAPI.Models;

public record TokenPairModel
{
    public string AccessToken { get; init; }
    public Guid RefreshToken { get; init; }
}