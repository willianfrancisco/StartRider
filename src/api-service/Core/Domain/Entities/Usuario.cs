namespace Domain.Entities;

public class Usuario
{
    public Usuario(string email, string password, string[] roles)
    {
        Email = email;
        Password = password;
        Roles = roles;
    }

    public int Id { get; private set; }
    public string Email { get; private set; } = "";
    public string Password { get; private set; } = "";
    public string[] Roles { get; private set; } =  Array.Empty<string>();
}