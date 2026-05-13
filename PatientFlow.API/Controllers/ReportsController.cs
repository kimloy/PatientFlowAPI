using Microsoft.AspNetCore.Mvc;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Reports;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("patients/{patientId}/summary")]
    public async Task<ActionResult<PatientSummaryReportResponse>>GetPatientSummary(Guid patientId)
    {
        var report = await _reportService
            .GetPatientSummaryAsync(patientId);

        if (report == null)
        {
            return NotFound();
        }

        return Ok(report);
    }
}