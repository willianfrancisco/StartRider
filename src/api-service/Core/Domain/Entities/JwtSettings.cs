namespace Domain.Entities;

public class JwtSettings
{
    public string SecretKey { get; set; } = "";
    public int ExpiryInMinutes { get; set; }
}