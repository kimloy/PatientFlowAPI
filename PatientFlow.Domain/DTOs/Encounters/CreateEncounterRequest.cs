using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Encounters;

public class CreateEncounterRequest
{
    public Guid PatientId { get; set; }

    public EncounterType EncounterType { get; set; }

    public EncounterStatus Status { get; set; }

    public string Department { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Reason { get; set; }
}