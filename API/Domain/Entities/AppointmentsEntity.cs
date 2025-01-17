using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Api.Utils.Enums;

namespace Api.Domain.Entities;

public class AppointmentsEntity 
{
    [Key]
    public int Id { get; set; } = default!;

    public int Patient_Id { get; set; } = default!;

    [ForeignKey(nameof(Patient_Id))]
    [JsonIgnore]
    public PatientsEntity Patient { get; set; } = default!;

    public int Doctor_Id { get; set; } = default!;

    [ForeignKey(nameof(Doctor_Id))]
    [JsonIgnore]
    public DoctorsEntity Doctor { get; set; } = default!;

    public int Doctors_Days_Available_Id { get; set; } = default!;

    [ForeignKey(nameof(Doctors_Days_Available_Id))]
    [JsonIgnore]
    public DoctorsDaysAvailableEntity Date { get; set; } = default!;

    [Required]
    public Status Status { get; set; } = default!;

    [Required]
    public AppointmentsTypes Type { get; set; } = default!;
}