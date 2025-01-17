using Api.Domain.Entities;
using Api.Utils.Enums;

namespace Api.Domain.DTOs;

public class AppointmentsDTO
{
    public int Patient_Id { get; set; } = default!;

    public int Doctor_Id { get; set; } = default!;

    public int Doctors_Days_Available_Id { get; set; } = default!;

    public Status Status { get; set; } = default!;

    public AppointmentsTypes Type { get; set; } = default!;
}