namespace Api.Domain.DTOs;

public class AdministratorLoginDTO 
{
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
}