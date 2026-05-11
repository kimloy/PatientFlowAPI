using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;
using PatientFlow.Domain.DTOs.Patients;

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
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients()
    {
        List<PatientResponse> patients = await _context.Patients
            .Select(patient => new PatientResponse
            {
                Id = patient.Id,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt
            })
            .ToListAsync();

        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponse>> GetPatientById(Guid id)
    {
        Patient? patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

        if (patient == null)
        {
            return NotFound();
        }

        var response = new PatientResponse
        {
            Id = patient.Id,
            MedicalRecordNumber = patient.MedicalRecordNumber,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PatientResponse>> CreatePatient(CreatePatientRequest request)
    {
        var patient = new Patient
        {
            MedicalRecordNumber = request.MedicalRecordNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        var response = new PatientResponse
        {
            Id = patient.Id,
            MedicalRecordNumber = patient.MedicalRecordNumber,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };

        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, response);
    }
}