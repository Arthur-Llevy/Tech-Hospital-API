using Api.Utils.Enums;

namespace Api.Domain.DTOs;

public class DoctorsDTO
{
    public string Name { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
    public DoctorsSpecialties Specialty { get; set; } = default!;
}