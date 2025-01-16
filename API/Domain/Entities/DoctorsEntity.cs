using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Utils.Enums;

namespace Api.Domain.Entities;

public class DoctorsEntity 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string User { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string Specialty { get; set; } = default!;

    public ICollection<DoctorsDaysAvailableEntity> Available_Days { get; set; } = default!; 

    public ICollection<AppointmentsEntity> Appointments { get; set; } = default!;
}