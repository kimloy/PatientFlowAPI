using Microsoft.AspNetCore.Mvc;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Fhir;
using PatientFlow.Domain.DTOs.Patients;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/fhir/patients")]
public class FhirPatientsController : ControllerBase
{
    private readonly IFhirPatientImportService _fhirPatientImportService;

    public FhirPatientsController(IFhirPatientImportService fhirPatientImportService)
    {
        _fhirPatientImportService = fhirPatientImportService;
    }

    [HttpPost("import")]
    public async Task<ActionResult<PatientResponse>> ImportPatient(
        FhirPatientRequest request)
    {
        try
        {
            var patient = await _fhirPatientImportService
                .ImportPatientAsync(request);

            return Ok(patient);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}