using Api.Domain.Entities;
using Api.Utils.Enums;

namespace Api.Domain.DTOs;

public class PatientsDTO
{
    public string Cpf { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Age { get; set; } = default!;
    public DateOnly Birth_Date { get; set; } = default!;
    public Gender Gender { get; set; } = default!;
    public string? Observations { get; set; }
    public ICollection<AppointmentsEntity?>? Appointments { get; set; } = default!;
}