using PatientFlow.Domain.Common;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.Entities;
public class Observation : BaseEntity
{
    public Guid PatientId { get; set; }

    public Patient? Patient { get; set; }

    public Guid? EncounterId { get; set; }

    public Encounter? Encounter { get; set; }

    public string ObservationType { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public ObservationStatus Status { get; set; } = ObservationStatus.Final;

    public DateTime RecordedAt { get; set; }
}