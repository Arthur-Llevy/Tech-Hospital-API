using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Entities;

public class DoctorsDaysAvailableEntity 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;

    [Required]
    public DateOnly Date { get; set; } = default!;

    [Required]
    public TimeOnly Arrival_Time { get; set; } = default!;

    [Required]
    public TimeOnly Departure_Time { get; set; } = default!;

    [Required]
    public int Doctor_Id { get; set; } = default!;

    [ForeignKey(nameof(Doctor_Id))]
    public DoctorsEntity Doctor { get; set; } = default!;
}