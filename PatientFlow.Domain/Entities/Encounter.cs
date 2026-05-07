using PatientFlow.Domain.Common;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.Entities;
public class Encounter : BaseEntity
{
    public Guid PatientId { get; set; }

    public Patient? Patient { get; set; } = null!;

    public EncounterType EncounterType { get; set; }

    public EncounterStatus Status { get; set; } = EncounterStatus.Scheduled;

    public string? Department { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Reason { get; set; } = string.Empty;

    public ICollection<Observation> Observations { get; set; } = new List<Observation>();

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}