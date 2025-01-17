using Api.Domain.Entities;

namespace Api.Domain.DTOs;

public class DoctorsDaysAvailableDTO
{
    public DateOnly Date { get; set; } = default!;
    public TimeOnly Arrival_Time { get; set; } = default!;
    public TimeOnly Departure_Time { get; set; } = default!;
    public int Doctor_Id { get; set; } = default!;
}