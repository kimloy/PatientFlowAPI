using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly PatientFlowDbContext _context;

    public PatientsController(PatientFlowDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        List<Patient> patients = await _context.Patients.ToListAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatientById(Guid id)
    {
        Patient? patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
    }
}