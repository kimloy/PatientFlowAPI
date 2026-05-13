using Microsoft.EntityFrameworkCore;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Reports;
using PatientFlow.Infrastructure.Data;

namespace PatientFlow.Application.Services;

public class ReportService : IReportService
{
    private readonly PatientFlowDbContext _context;

    public ReportService(PatientFlowDbContext context)
    {
        _context = context;
    }

    public async Task<PatientSummaryReportResponse?> GetPatientSummaryAsync(Guid patientId)
    {
        return await _context.Patients
            .Where(p => p.Id == patientId)
            .Select(p => new PatientSummaryReportResponse
            {
                PatientId = p.Id,
                PatientName = $"{p.FirstName} {p.LastName}",
                MedicalRecordNumber = p.MedicalRecordNumber,

                TotalEncounters = p.Encounters.Count(),
                TotalObservations = p.Observations.Count(),

                LatestEncounterDate = p.Encounters
                    .OrderByDescending(e => e.StartTime)
                    .Select(e => (DateTime?)e.StartTime)
                    .FirstOrDefault(),

                LatestObservationDate = p.Observations
                    .OrderByDescending(o => o.RecordedAt)
                    .Select(o => (DateTime?)o.RecordedAt)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }
}