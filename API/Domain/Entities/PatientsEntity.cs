using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Utils.Enums;

namespace Api.Domain.Entities;

public class PatientsEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;

    [Required]
    public string Cpf { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public int Age { get; set; } = default!;

    [Required]
    public DateOnly Birth_Date { get; set; } = default!;

    [Required]
    public Gender Gender { get; set; } = default!;

    public string? Observations { get; set; }

    public ICollection<AppointmentsEntity> Appointments { get; set; } = default!;
}   