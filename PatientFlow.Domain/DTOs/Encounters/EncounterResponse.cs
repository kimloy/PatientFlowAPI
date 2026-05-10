using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Encounters;

public class EncounterResponse
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public PatientResponse? Patient { get; set; }

    public EncounterType EncounterType { get; set; }

    public EncounterStatus Status { get; set; }

    public string Department { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}