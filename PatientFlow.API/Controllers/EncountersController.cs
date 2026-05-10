using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;

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
    public async Task<ActionResult<Encounter>> GetEncounterById(Guid id)
    {
        Encounter? encounter = await _context.Encounters
        .Include(e => e.Patient)
        .FirstOrDefaultAsync(e => e.Id == id);

        if(encounter == null)
        {
            return NotFound();
        }

        return Ok(encounter);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<Encounter>>> GetEncountersByPatientId(Guid patientId)
    {
        var encounters = await _context.Encounters
        .Where(e => e.PatientId == patientId)
        .ToListAsync();

        return Ok(encounters);
    }

    [HttpPost]
    public async Task<ActionResult<Encounter>> CreateEncounter(Encounter encounter)
    {
       bool patientExists = await _context.Patients.AnyAsync(p => p.Id == encounter.PatientId);

       if(!patientExists)
       {
           return BadRequest($"Patient with ID {encounter.PatientId} does not exist.");
       }

       _context.Encounters.Add(encounter);

       await _context.SaveChangesAsync();

       return CreatedAtAction(nameof(GetEncounterById), new { id = encounter.Id }, encounter);
    }
}