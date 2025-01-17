using Api.Domain.Entities;
using Api.Utils.Enums;

namespace Api.Domain.ModelsViews;

public class AppointmentsModelView
{
    public int Id { get; set; } = default!;
    public int Patient_Id { get; set; } = default!;

    public int Doctor_Id { get; set; } = default!;

    public int Doctors_Days_Available_Id { get; set; } = default!;

    public DoctorsDaysAvailableEntity Date { get; set; } = default!;
    public DoctorsEntity Doctor { get; set; } = default!;
    public PatientsEntity Patient { get; set; } = default!;

    public string Status { get; set; } = default!;

    public string Type { get; set; } = default!;
}