namespace Api.Domain.DTOs;

public class LoginDTO 
{
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
}