using PatientFlow.Domain.Common;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.Entities;

public class ServiceRequest : BaseEntity
{
    public Guid PatientId { get; set; }

    public Patient? Patient { get; set; }

    public Guid? EncounterId { get; set; }

    public Encounter? Encounter { get; set; }

    public string RequestType { get; set; } = string.Empty;

    public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Active;

    public ServiceRequestPriority Priority { get; set; } = ServiceRequestPriority.Routine;

    public DateTime OrderedAt { get; set; }

    public string? Notes { get; set; }
}