using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Observations;

public class ObservationResponse
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public PatientResponse? Patient { get; set; }

    public Guid? EncounterId { get; set; }

    public string ObservationType { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public ObservationStatus Status { get; set; }

    public DateTime RecordedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}