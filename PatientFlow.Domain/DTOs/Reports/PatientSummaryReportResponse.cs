namespace PatientFlow.Domain.DTOs.Reports;

public class PatientSummaryReportResponse
{
    public Guid PatientId { get; set; }

    public string PatientName { get; set; } = string.Empty;

    public string MedicalRecordNumber { get; set; } = string.Empty;

    public int TotalEncounters { get; set; }

    public int TotalObservations { get; set; }

    public DateTime? LatestEncounterDate { get; set; }

    public DateTime? LatestObservationDate { get; set; }
}