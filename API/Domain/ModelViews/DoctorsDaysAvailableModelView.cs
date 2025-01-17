using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Api.Domain.Entities;

namespace Api.Domain.ModelsViews;

public class DoctorsDaysAvailableModelView
{
    public int Id { get; set; } = default!;
    public DateOnly Date { get; set; } = default!;
    public TimeOnly Arrival_Time { get; set; } = default!;
    public TimeOnly Departure_Time { get; set; } = default!;
    public int Doctor_Id { get; set; } = default!;
    public DoctorsEntity Doctor { get; set; } = default!;
}