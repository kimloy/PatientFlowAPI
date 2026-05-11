using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;
using PatientFlow.Domain.DTOs.Encounters;
using PatientFlow.Domain.DTOs.Patients;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EncountersController : ControllerBase
{
    readonly PatientFlowDbContext _context;

    public EncountersController(PatientFlowDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EncounterResponse>> GetEncounterById(Guid id)
    {
        Encounter? encounter = await _context.Encounters
        .Include(e => e.Patient)
        .FirstOrDefaultAsync(e => e.Id == id);

        if(encounter == null)
        {
            return NotFound();
        }

        var response = new EncounterResponse
        {
            Id = encounter.Id,
            PatientId = encounter.PatientId,

            Patient = encounter.Patient == null
                ? null
                : new PatientResponse
                {
                    Id = encounter.Patient.Id,
                    MedicalRecordNumber = encounter.Patient.MedicalRecordNumber,
                    FirstName = encounter.Patient.FirstName,
                    LastName = encounter.Patient.LastName,
                    DateOfBirth = encounter.Patient.DateOfBirth,
                    Gender = encounter.Patient.Gender,
                    PhoneNumber = encounter.Patient.PhoneNumber,
                    Email = encounter.Patient.Email,
                    CreatedAt = encounter.Patient.CreatedAt,
                    UpdatedAt = encounter.Patient.UpdatedAt
                },

            EncounterType = encounter.EncounterType,
            Status = encounter.Status,
            Department = encounter.Department,
            StartTime = encounter.StartTime,
            EndTime = encounter.EndTime,
            Reason = encounter.Reason,
            CreatedAt = encounter.CreatedAt,
            UpdatedAt = encounter.UpdatedAt
        };

        return Ok(response);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<Encounter>>> GetEncountersByPatientId(Guid patientId)
    {
       var encounters = await _context.Encounters
        .Include(e => e.Patient)
        .Where(e => e.PatientId == patientId)
        .Select(encounter => new EncounterResponse
        {
            Id = encounter.Id,
            PatientId = encounter.PatientId,

            Patient = encounter.Patient == null
                ? null
                : new PatientResponse
                {
                    Id = encounter.Patient.Id,
                    MedicalRecordNumber = encounter.Patient.MedicalRecordNumber,
                    FirstName = encounter.Patient.FirstName,
                    LastName = encounter.Patient.LastName,
                    DateOfBirth = encounter.Patient.DateOfBirth,
                    Gender = encounter.Patient.Gender,
                    PhoneNumber = encounter.Patient.PhoneNumber,
                    Email = encounter.Patient.Email,
                    CreatedAt = encounter.Patient.CreatedAt,
                    UpdatedAt = encounter.Patient.UpdatedAt
                },

            EncounterType = encounter.EncounterType,
            Status = encounter.Status,
            Department = encounter.Department,
            StartTime = encounter.StartTime,
            EndTime = encounter.EndTime,
            Reason = encounter.Reason,
            CreatedAt = encounter.CreatedAt,
            UpdatedAt = encounter.UpdatedAt
        })
        .ToListAsync();

        return Ok(encounters);
    }

    [HttpPost]
    public async Task<ActionResult<EncounterResponse>> CreateEncounter(CreateEncounterRequest request)
    {
    var patientExists = await _context.Patients
        .AnyAsync(p => p.Id == request.PatientId);

    if (!patientExists)
    {
        return BadRequest("Patient does not exist.");
    }

    var encounter = new Encounter
    {
        PatientId = request.PatientId,
        EncounterType = request.EncounterType,
        Status = request.Status,
        Department = request.Department,
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        Reason = request.Reason
    };

    _context.Encounters.Add(encounter);
    await _context.SaveChangesAsync();

    var response = new EncounterResponse
    {
        Id = encounter.Id,
        PatientId = encounter.PatientId,
        EncounterType = encounter.EncounterType,
        Status = encounter.Status,
        Department = encounter.Department,
        StartTime = encounter.StartTime,
        EndTime = encounter.EndTime,
        Reason = encounter.Reason,
        CreatedAt = encounter.CreatedAt,
        UpdatedAt = encounter.UpdatedAt
    };

    return CreatedAtAction(nameof(GetEncounterById), new { id = encounter.Id }, response);
    }
}