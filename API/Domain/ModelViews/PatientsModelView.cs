using Api.Domain.Entities;
using Api.Utils.Enums;

namespace Api.Domain.ModelsViews;

public class PatientsModelView
{
    public int Id { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Age { get; set; } = default!;
    public DateOnly Birth_Date { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string? Observations { get; set; }
    public ICollection<AppointmentsEntity?> Appointments { get; set; } = default!;
}