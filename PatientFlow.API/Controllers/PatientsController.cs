using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;
using PatientFlow.Application.Interfaces;
using PatientFlow.Infrastructure.Data;
using PatientFlow.Domain.DTOs.Patients;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients()
    {
        var patients = await _patientService.GetPatientsAsync();

        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponse>> GetPatientById(Guid id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<PatientResponse>> CreatePatient(CreatePatientRequest request)
    {
        var patient = await _patientService.CreatePatientAsync(request);

        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
    }
}