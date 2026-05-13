using System.ComponentModel.DataAnnotations;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Observations;

public class CreateObservationRequest
{
    [Required]
    public Guid PatientId { get; set; }

    public Guid? EncounterId { get; set; }

    [Required]
    [StringLength(100)]
    public string ObservationType { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Value { get; set; } = string.Empty;

    [Required]
    [StringLength(25)]
    public string Unit { get; set; } = string.Empty;

    [Required]
    public ObservationStatus Status { get; set; }

    [Required]
    public DateTime RecordedAt { get; set; }
}