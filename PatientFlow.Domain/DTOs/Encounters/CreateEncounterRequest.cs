using System.ComponentModel.DataAnnotations;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Encounters;

public class CreateEncounterRequest
{
    [Required]
    public Guid PatientId { get; set; }

    [Required]
    public EncounterType EncounterType { get; set; }

    [Required]
    public EncounterStatus Status { get; set; }

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [StringLength(250)]
    public string? Reason { get; set; }
}