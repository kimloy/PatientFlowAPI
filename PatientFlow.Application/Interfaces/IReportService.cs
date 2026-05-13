using PatientFlow.Domain.DTOs.Reports;

namespace PatientFlow.Application.Interfaces;

public interface IReportService
{
    Task<PatientSummaryReportResponse?> GetPatientSummaryAsync(Guid patientId);
}